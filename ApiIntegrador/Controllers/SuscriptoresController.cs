using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiIntegrador.Data;
using ApiIntegrador.Dtos;

namespace ApiIntegrador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuscriptoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SuscriptoresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuscriptorDto>>> GetSuscriptores()
        {
            var suscriptores = await _context.Suscriptores
                .Select(s => new SuscriptorDto
                {
                    IdSuscriptor = s.IdSuscriptor,
                    Nombre = s.Nombre,
                    IdPaquete = s.IdPaquete,
                    EsNuevo = s.EsNuevo,
                    IdColonia = s.IdColonia
                })
                .ToListAsync();

            return Ok(suscriptores);
        }
    }
}
