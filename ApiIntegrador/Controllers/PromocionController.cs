using ApiIntegrador.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiIntegrador.Models;
using ApiIntegrador.Dto;
using ApiIntegrador.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiIntegrador.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class PromocionesController : ControllerBase
{
    private readonly PromocionService _promocionService;

    public PromocionesController(PromocionService promocionService)
    {
        _promocionService = promocionService;
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearPromocionDTO dto)
    {
        var promo = await _promocionService.CrearPromocionAsync(dto);
        return Ok(promo);
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? ciudad, [FromQuery] string? colonia, [FromQuery] int? paqueteId, [FromQuery] string? tipoServicio)
    {
        var promos = await _promocionService.ObtenerPromocionesAsync(ciudad, colonia, paqueteId, tipoServicio);
        return Ok(promos);
    }

    [HttpPut("{id}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromQuery] bool activa)
    {
        var ok = await _promocionService.CambiarEstadoPromocionAsync(id, activa);
        return ok ? Ok() : NotFound();
    }
}
}