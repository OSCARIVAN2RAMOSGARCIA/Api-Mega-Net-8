using ApiIntegrador.Dto;
using ApiIntegrador.Models;
using ApiIntegrador.Data;
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
                TipoContrato = contrato.TipoContrato ?? "Desconocido",
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
                        DescuentoAplicado = monto,
                        FechaInicio = pa.FechaAplicacion,
                        FechaFin = fechaFin,
                        DiasDuracion = (fechaFin - pa.FechaAplicacion).Days
                    });
                }

                listaPagos.Add(new SuscriptorPagoDTO
                {
                    Nombre = contrato.Suscriptor?.Nombre ?? "Sin nombre",
                    Paquete = contrato.Paquete?.Nombre ?? "Sin paquete",
                    TipoContrato = contrato.TipoContrato ?? "Desconocido",
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

        public async Task<SuscriptorDeudaDTO?> CalcularDeudaTotalAsync(int idSuscriptor)
        {
            var hoy = System.DateTime.Today;

            var contratos = await _context.Contratos
                .Include(c => c.Paquete)
                    .ThenInclude(p => p.PaqueteServicios)
                        .ThenInclude(ps => ps.Servicio)
                .Include(c => c.PromocionesAplicadas)
                    .ThenInclude(pa => pa.Promocion)
                .Include(c => c.Suscriptor)
                    .ThenInclude(s => s.Colonia)
                        .ThenInclude(col => col.Ciudad)
                .Where(c => c.IdSuscriptor == idSuscriptor && c.Activo)
                .ToListAsync();

            if (!contratos.Any()) return null;

            var deudaTotal = 0m;
            var contratosDetalles = new List<ContratoDeudaDetalleDTO>();

            foreach (var contrato in contratos)
            {
                var precioBase = contrato.TipoContrato.ToLower() == "empresarial"
                    ? contrato.Paquete.PaqueteServicios.Sum(ps => ps.Servicio.PrecioEmpresarial)
                    : contrato.Paquete.PaqueteServicios.Sum(ps => ps.Servicio.PrecioResidencial);

                var promocionesValidas = contrato.PromocionesAplicadas
                    .Where(pa =>
                        pa.Promocion != null &&
                        pa.Promocion.Activa &&
                        pa.Promocion.VigenciaDesde <= hoy &&
                        pa.Promocion.VigenciaHasta >= hoy &&
                        (
                            pa.PromocionConfiguracion == null ||
                            (
                                (pa.PromocionConfiguracion.IdCiudad == null || pa.PromocionConfiguracion.IdCiudad == contrato.Suscriptor.Colonia.IdCiudad) &&
                                (pa.PromocionConfiguracion.IdColonia == null || pa.PromocionConfiguracion.IdColonia == contrato.Suscriptor.IdColonia) &&
                                (pa.PromocionConfiguracion.IdPaquete == null || pa.PromocionConfiguracion.IdPaquete == contrato.IdPaquete)
                            )
                        )
                    )
                    .ToList();

                decimal descuentoTotal = 0;
                var promocionesDetalle = new List<PromocionDetalleDTO>();

                foreach (var pa in promocionesValidas)
                {
                    var monto = pa.DescuentoAplicado;
                    var fechaFin = pa.FechaTermino ?? pa.Promocion.VigenciaHasta;

                    if (monto > 0 && monto < 1)
                        descuentoTotal += precioBase * monto;
                    else
                        descuentoTotal += monto;

                    promocionesDetalle.Add(new PromocionDetalleDTO
                    {
                        Nombre = pa.Promocion.Nombre,
                        DescuentoAplicado = monto,
                        FechaInicio = pa.FechaAplicacion,
                        FechaFin = fechaFin,
                        DiasDuracion = (fechaFin - pa.FechaAplicacion).Days
                    });
                }

                var totalAPagar = System.Math.Max(0, precioBase - descuentoTotal);
                deudaTotal += totalAPagar;

                contratosDetalles.Add(new ContratoDeudaDetalleDTO
                {
                    IdContrato = contrato.IdContrato,
                    Paquete = contrato.Paquete.Nombre,
                    TipoContrato = contrato.TipoContrato,
                    PrecioOriginal = precioBase,
                    DescuentoTotal = descuentoTotal,
                    TotalAPagar = totalAPagar,
                    Promociones = promocionesDetalle
                });
            }

            return new SuscriptorDeudaDTO
            {
                IdSuscriptor = idSuscriptor,
                Nombre = contratos.First().Suscriptor.Nombre,
                DeudaTotal = deudaTotal,
                Contratos = contratosDetalles
            };
        }
    }
}
