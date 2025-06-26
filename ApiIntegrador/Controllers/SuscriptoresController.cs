using ApiIntegrador.Data;
using ApiIntegrador.Dto;
using ApiIntegrador.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiIntegrador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuscriptoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SuscriptoresController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuscriptorDto>>> GetSuscriptores()
        {
            var suscriptores = await _context.Suscriptores
                .Include(c => c.Colonia)
                .ThenInclude(col => col.Ciudad)
                .ToListAsync();

            var suscriptoresDto = suscriptores.Select(x => new SuscriptorDto
            {
                IdSuscriptor = x.IdSuscriptor,
                Nombre = x.Nombre,
                IdColonia = x.IdColonia,
                ColoniaNombre = x.Colonia.Nombre, // Cambiado de NombreColonia a Nombre
                CiudadNombre = x.Colonia.Ciudad.Nombre, // Cambiado de NombreCiudad a Nombre
                FechaRegistro = x.FechaRegistro
            }).ToList();

            return Ok(suscriptoresDto);
        }
    }
}