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

            decimal precioBase = contrato.TipoContrato?.ToLower() == "empresarial"
                ? contrato.Paquete?.PaqueteServicios.Sum(ps => ps.Servicio.PrecioEmpresarial) ?? 0
                : contrato.Paquete?.PaqueteServicios.Sum(ps => ps.Servicio.PrecioResidencial) ?? 0;

            decimal totalDescuento = 0m;
            List<string> porcentajes = new();
            List<PromocionDetalleDTO> promocionesDetalles = new();

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
                DateTime fechaFin = pa.FechaTermino ?? pa.Promocion!.VigenciaHasta;

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

                promocionesDetalles.Add(new PromocionDetalleDTO
                {
                    Nombre = pa.Promocion!.Nombre,
                    DescuentoAplicado = monto,
                    FechaInicio = pa.FechaAplicacion,
                    FechaFin = fechaFin,
                    DiasDuracion = (fechaFin - pa.FechaAplicacion).Days
                });
            }

            return new SuscriptorPagoDTO
            {
                Nombre = contrato.Suscriptor?.Nombre ?? "Sin nombre",
                Paquete = contrato.Paquete?.Nombre ?? "Sin paquete",
                PrecioOriginal = precioBase,
                Descuento = totalDescuento,
                DescripcionPromocion = string.Join(", ", promocionesValidas.Select(p => p.Promocion!.Nombre)),
                TotalAPagar = System.Math.Max(0, precioBase - totalDescuento),
                PorcentajeAplicado = string.Join(", ", porcentajes),
                PromocionesDetalles = promocionesDetalles
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
                decimal precioBase = contrato.TipoContrato?.ToLower() == "empresarial"
                    ? contrato.Paquete?.PaqueteServicios.Sum(ps => ps.Servicio.PrecioEmpresarial) ?? 0
                    : contrato.Paquete?.PaqueteServicios.Sum(ps => ps.Servicio.PrecioResidencial) ?? 0;

                decimal totalDescuento = 0m;
                List<string> porcentajes = new();
                List<PromocionDetalleDTO> promocionesDetalles = new();

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
                    DateTime fechaFin = pa.FechaTermino ?? pa.Promocion!.VigenciaHasta;

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

                    promocionesDetalles.Add(new PromocionDetalleDTO
                    {
                        Nombre = pa.Promocion!.Nombre,
                        DescuentoAplicado =monto,
                        FechaInicio = pa.FechaAplicacion,
                        FechaFin = fechaFin,
                        DiasDuracion = (fechaFin - pa.FechaAplicacion).Days
                    });
                }

                listaPagos.Add(new SuscriptorPagoDTO
                {
                    Nombre = contrato.Suscriptor?.Nombre ?? "Sin nombre",
                    Paquete = contrato.Paquete?.Nombre ?? "Sin paquete",
                    PrecioOriginal = precioBase,
                    Descuento = totalDescuento,
                    DescripcionPromocion = string.Join(", ", promocionesValidas.Select(p => p.Promocion!.Nombre)),
                    TotalAPagar = System.Math.Max(0, precioBase - totalDescuento),
                    PorcentajeAplicado = string.Join(", ", porcentajes),
                    PromocionesDetalles = promocionesDetalles
                });
            }

            return listaPagos;
        }
    }
}
