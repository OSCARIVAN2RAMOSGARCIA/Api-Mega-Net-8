using ApiIntegrador.Data;
using ApiIntegrador.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrador.Services
{
    public class ContratoService
    {
        private readonly AppDbContext _context;

        public ContratoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SuscriptorPagoDTO?> CalcularPagoMensualAsync(int idSuscriptor)
        {
            var hoy = System.DateTime.Today;

            var contrato = await _context.Contratos
                .Include(c => c.Paquete)
                    .ThenInclude(p => p.PaqueteServicios)
                        .ThenInclude(ps => ps.Servicio)
                .Include(c => c.PromocionesAplicadas)
                    .ThenInclude(pa => pa.Promocion)
                .Include(c => c.Suscriptor)
                .Where(c => c.IdSuscriptor == idSuscriptor && c.Activo)
                .FirstOrDefaultAsync();

            if (contrato == null) return null;

            // Elegir precio según tipo de contrato
            decimal precioBase = 0m;
            if (contrato.TipoContrato?.ToLower() == "empresarial")
            {
                precioBase = contrato.Paquete?.PaqueteServicios.Sum(ps => ps.Servicio.PrecioEmpresarial) ?? 0m;
            }
            else
            {
                precioBase = contrato.Paquete?.PaqueteServicios.Sum(ps => ps.Servicio.PrecioResidencial) ?? 0m;
            }

            decimal totalDescuento = 0m;
            List<string> porcentajes = new();

            var promocionesValidas = contrato.PromocionesAplicadas
                .Where(pa =>
                    pa.Promocion != null &&
                    pa.Promocion.Activa &&
                    pa.Promocion.VigenciaDesde <= hoy &&
                    pa.Promocion.VigenciaHasta >= hoy)
                .ToList();

            foreach (var pa in promocionesValidas)
            {
                var monto = pa.DescuentoAplicado;
                if (monto > 0 && monto < 1m)
                {
                    totalDescuento += precioBase * monto;
                    porcentajes.Add((monto * 100m).ToString("0.##") + "%");
                }
                else if (monto >= 1m)
                {
                    totalDescuento += monto;
                    porcentajes.Add(monto.ToString("C")); // muestra como moneda
                }
            }

            return new SuscriptorPagoDTO
            {
                Nombre = contrato.Suscriptor?.Nombre ?? "Sin nombre",
                Paquete = contrato.Paquete?.Nombre ?? "Sin paquete",
                PrecioOriginal = precioBase,
                Descuento = totalDescuento,
                DescripcionPromocion = string.Join(", ", promocionesValidas.Select(p => p.Promocion!.Nombre)),
                TotalAPagar = System.Math.Max(0, precioBase - totalDescuento),
                PorcentajeAplicado = string.Join(", ", porcentajes)
            };
        }

        public async Task<List<SuscriptorPagoDTO>> CalcularPagosMensualesAsync()
        {
            var hoy = System.DateTime.Today;

            var contratos = await _context.Contratos
                .Include(c => c.Paquete)
                    .ThenInclude(p => p.PaqueteServicios)
                        .ThenInclude(ps => ps.Servicio)
                .Include(c => c.PromocionesAplicadas)
                    .ThenInclude(pa => pa.Promocion)
                .Include(c => c.Suscriptor)
                .Where(c => c.Activo)
                .AsNoTracking()
                .ToListAsync();

            var listaPagos = new List<SuscriptorPagoDTO>();

            foreach (var contrato in contratos)
            {
                decimal precioBase = 0m;
                if (contrato.TipoContrato?.ToLower() == "empresarial")
                {
                    precioBase = contrato.Paquete?.PaqueteServicios.Sum(ps => ps.Servicio.PrecioEmpresarial) ?? 0m;
                }
                else
                {
                    precioBase = contrato.Paquete?.PaqueteServicios.Sum(ps => ps.Servicio.PrecioResidencial) ?? 0m;
                }

                decimal totalDescuento = 0m;
                List<string> porcentajes = new();

                var promocionesValidas = contrato.PromocionesAplicadas
                    .Where(pa =>
                        pa.Promocion != null &&
                        pa.Promocion.Activa &&
                        pa.Promocion.VigenciaDesde <= hoy &&
                        pa.Promocion.VigenciaHasta >= hoy)
                    .ToList();

                foreach (var pa in promocionesValidas)
                {
                    var monto = pa.DescuentoAplicado;
                    if (monto > 0 && monto < 1m)
                    {
                        totalDescuento += precioBase * monto;
                        porcentajes.Add((monto * 100m).ToString("0.##") + "%");
                    }
                    else if (monto >= 1m)
                    {
                        totalDescuento += monto;
                        porcentajes.Add(monto.ToString("C"));
                    }
                }

                listaPagos.Add(new SuscriptorPagoDTO
                {
                    Nombre = contrato.Suscriptor?.Nombre ?? "Sin nombre",
                    Paquete = contrato.Paquete?.Nombre ?? "Sin paquete",
                    PrecioOriginal = precioBase,
                    Descuento = totalDescuento,
                    DescripcionPromocion = string.Join(", ", promocionesValidas.Select(p => p.Promocion!.Nombre)),
                    TotalAPagar = System.Math.Max(0, precioBase - totalDescuento),
                    PorcentajeAplicado = string.Join(", ", porcentajes)
                });
            }

            return listaPagos;
        }
    }
}
