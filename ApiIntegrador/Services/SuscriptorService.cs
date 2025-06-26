using ApiIntegrador.Data;
using ApiIntegrador.Dto;
using Microsoft.EntityFrameworkCore;

public class SuscriptorService
{
    private readonly AppDbContext _context;

    public SuscriptorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DetalleSuscriptorDTO?> ObtenerDetalleAsync(int idSuscriptor)
    {
        var hoy = DateTime.Today;

        var contrato = await _context.Contratos
            .Include(c => c.Paquete)
                .ThenInclude(p => p.PaqueteServicios)
                    .ThenInclude(ps => ps.Servicio)
            .Include(c => c.PromocionesAplicadas)
                .ThenInclude(pa => pa.Promocion)
            .Include(c => c.Suscriptor)
                .ThenInclude(s => s.Colonia)
                    .ThenInclude(col => col.Ciudad)
            .FirstOrDefaultAsync(c => c.IdSuscriptor == idSuscriptor && c.Activo);

        if (contrato == null)
            return null;

        var suscriptor = contrato.Suscriptor!;
        var servicios = contrato.Paquete?.PaqueteServicios
            .Select(ps => ps.Servicio.Nombre)
            .ToList() ?? new List<string>();

        decimal precioOriginal = contrato.Paquete?.PaqueteServicios.Sum(ps => ps.Servicio.PrecioResidencial) ?? 0;
        decimal descuento = 0;
        List<string> promociones = new();

        var promocionesValidas = contrato.PromocionesAplicadas
            .Where(pa => pa.Promocion != null &&
                         pa.Promocion.Activa &&
                         pa.Promocion.VigenciaDesde <= hoy &&
                         pa.Promocion.VigenciaHasta >= hoy)
            .ToList();

        foreach (var pa in promocionesValidas)
        {
            var monto = pa.DescuentoAplicado;
            if (monto < 1)
                descuento += precioOriginal * monto;
            else
                descuento += monto;

            promociones.Add(pa.Promocion!.Nombre);
        }

        return new DetalleSuscriptorDTO
        {
            IdSuscriptor = suscriptor.IdSuscriptor,
            Nombre = suscriptor.Nombre,
            Colonia = suscriptor.Colonia.Nombre,
            Ciudad = suscriptor.Colonia.Ciudad.Nombre,
            Paquete = contrato.Paquete?.Nombre ?? "Sin paquete",
            Servicios = servicios,
            PrecioOriginal = precioOriginal,
            Descuento = descuento,
            TotalAPagar = Math.Max(0, precioOriginal - descuento),
            Promociones = promociones
        };
    }
}