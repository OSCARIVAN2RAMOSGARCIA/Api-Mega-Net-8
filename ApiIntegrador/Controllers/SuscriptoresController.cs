using ApiIntegrador.Data;
using ApiIntegrador.Dto;
using ApiIntegrador.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiIntegrador.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class SuscriptoresController : ControllerBase
{
    private readonly SuscriptorService _suscriptorService;

    public SuscriptoresController(SuscriptorService suscriptorService)
    {
        _suscriptorService = suscriptorService;
    }

    [HttpGet("{id}/detalle")]
    public async Task<ActionResult<DetalleSuscriptorDTO>> ObtenerDetalle(int id)
    {
        var resultado = await _suscriptorService.ObtenerDetalleAsync(id);
        if (resultado == null)
            return NotFound(new { mensaje = $"No se encontró el suscriptor con ID {id} o no tiene contrato activo." });

        return Ok(resultado);
    }
}

}