using ApiIntegrador.Data;
using ApiIntegrador.Models;
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

        public async Task<List<PromocionDTO>> ObtenerPromocionesConDetallesAsync(string? tipoServicio = null)
        {
            var promociones = await _context.Promociones.ToListAsync();
            var result = new List<PromocionDTO>();

            foreach (var promo in promociones)
            {
                if (!string.IsNullOrEmpty(tipoServicio) &&
                    promo.TipoPromocion.ToLower() != tipoServicio.ToLower() &&
                    promo.TipoPromocion.ToLower() != "ambos")
                {
                    continue;
                }

                var configuraciones = await _context.PromocionConfiguraciones
                    .Where(pc => pc.IdPromocion == promo.IdPromocion)
                    .ToListAsync();

                var ciudades = configuraciones
                    .Where(pc => pc.IdCiudad != null)
                    .Select(pc => _context.Ciudades.FirstOrDefault(c => c.IdCiudad == pc.IdCiudad)?.Nombre)
                    .Where(nombre => nombre != null)
                    .Distinct()
                    .ToList();

                var paquetes = configuraciones
                    .Where(pc => pc.IdPaquete != null)
                    .Select(pc => _context.Paquetes.FirstOrDefault(p => p.IdPaquete == pc.IdPaquete)?.Nombre)
                    .Where(nombre => nombre != null)
                    .Distinct()
                    .ToList();

                result.Add(new PromocionDTO
                {
                    IdPromocion = promo.IdPromocion,
                    Nombre = promo.Nombre,
                    TipoPromocion = promo.TipoPromocion,
                    DescuentoResidencial = promo.DescuentoResidencial,
                    DescuentoEmpresarial = promo.DescuentoEmpresarial,
                    AplicaNuevos = promo.AplicaNuevos,
                    VigenciaDesde = promo.VigenciaDesde,
                    VigenciaHasta = promo.VigenciaHasta,
                    Activa = promo.Activa,
                    Ciudades = ciudades,
                    Paquetes = paquetes
                });
            }

            return result;
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

            if (dto.IdCiudad.HasValue)
            {
                var config = new PromocionConfiguracion
                {
                    IdPromocion = promocion.IdPromocion,
                    IdCiudad = dto.IdCiudad.Value
                };

                _context.PromocionConfiguraciones.Add(config);
                await _context.SaveChangesAsync();
            }

            return promocion;
        }

        public async Task<bool> CambiarEstadoPromocionAsync(int idPromocion, bool activa)
        {
            try
            {
                var promo = await _context.Promociones.FindAsync(idPromocion);
                if (promo == null) return false;

                promo.Activa = activa;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar estado: {ex.Message}");
                return false;
            }
        }

        public async Task<string?> ObtenerNombrePaquetePorId(int idPaquete)
{
    return await _context.Paquetes
        .Where(p => p.IdPaquete == idPaquete)
        .Select(p => p.Nombre)
        .FirstOrDefaultAsync();
}

    }
}
