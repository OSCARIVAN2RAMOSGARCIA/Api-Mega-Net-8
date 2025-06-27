using ApiIntegrador.Data;
using ApiIntegrador.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrador.Services
{
    public class PromocionService
{
    private readonly AppDbContext _context;

    public PromocionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Promocion>> ObtenerPromocionesAsync(string? ciudad = null, string? colonia = null, int? paqueteId = null, string? tipoServicio = null)
    {
        var query = _context.Promociones.AsQueryable();

        if (!string.IsNullOrEmpty(tipoServicio))
            query = query.Where(p => p.TipoPromocion == tipoServicio || p.TipoPromocion == "Ambos");

        // Aquí podrías unir con PromocionesAplicadas -> Contratos -> Suscriptores -> Colonias -> Ciudades
        // para filtrar por ciudad/colonia/paquete (si la promoción fue aplicada)
        // o puedes tener una tabla adicional de "PromocionConfiguracion" para que promociones estén
        // disponibles para ciertas zonas.

        return await query.ToListAsync();
    }

    public async Task<Promocion> CrearPromocionAsync(CrearPromocionDTO dto)
    {
        var promocion = new Promocion
        {
            Nombre = dto.Nombre,
            DescuentoResidencial = dto.DescuentoResidencial,
            DescuentoEmpresarial = dto.DescuentoEmpresarial,
            AplicaNuevos = dto.AplicaNuevos,
            TipoPromocion = dto.TipoPromocion,
            VigenciaDesde = dto.VigenciaDesde,
            VigenciaHasta = dto.VigenciaHasta,
            Activa = dto.Activa
        };

        _context.Promociones.Add(promocion);
        await _context.SaveChangesAsync();
        return promocion;
    }

    public async Task<bool> CambiarEstadoPromocionAsync(int idPromocion, bool activa)
    {
        var promo = await _context.Promociones.FindAsync(idPromocion);
        if (promo == null) return false;

        promo.Activa = activa;
        await _context.SaveChangesAsync();
        return true;
    }
}
}
