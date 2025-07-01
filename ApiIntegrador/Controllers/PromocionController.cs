using ApiIntegrador.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiIntegrador.Data;
using ApiIntegrador.Dto;
using System.Threading.Tasks;
using System.Linq;

namespace ApiIntegrador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromocionesController : ControllerBase
    {
        private readonly PromocionService _promocionService;
        private readonly AppDbContext _context;

        public PromocionesController(PromocionService promocionService, AppDbContext context)
        {
            _promocionService = promocionService;
            _context = context;
        }

[HttpPost]
public async Task<IActionResult> Crear([FromBody] CrearPromocionDTO dto)
{
    try
    {
        await _promocionService.CrearPromocionAsync(dto);
        return Ok();
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error al crear promoción: {ex.Message}");
    }
}


[HttpGet]
public async Task<IActionResult> Listar(
    [FromQuery] string? ciudad,
    [FromQuery] string? colonia,
    [FromQuery] int? paqueteId,
    [FromQuery] string? tipoServicio)
{
    var promociones = await _promocionService.ObtenerPromocionesConDetallesAsync(tipoServicio);

    if (!string.IsNullOrEmpty(ciudad))
    {
        promociones = promociones
            .Where(p => p.Ciudades.Any(c => c != null && c.ToLower().Contains(ciudad.ToLower())))
            .ToList();
    }

    if (paqueteId.HasValue)
    {
        var paqueteNombre = await _promocionService.ObtenerNombrePaquetePorId(paqueteId.Value);
        promociones = promociones
            .Where(p => p.Paquetes.Any(pk => pk == paqueteNombre))
            .ToList();
    }

    return Ok(promociones);
}
        [HttpPut("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromQuery] bool activa)
        {
            var ok = await _promocionService.CambiarEstadoPromocionAsync(id, activa);
            return ok ? Ok() : NotFound();
        }

        [HttpGet("ciudades")]
        public async Task<IActionResult> ObtenerCiudades()
        {
            try
            {
                var ciudades = await _context.Ciudades
                    .Select(c => new { c.IdCiudad, c.Nombre })
                    .ToListAsync();

                return Ok(ciudades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener ciudades: {ex.Message}");
            }
        }
    }
}