using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiIntegrador.Data;
using ApiIntegrador.Dtos;

namespace ApiIntegrador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaquetesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaquetesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaqueteDto>>> GetPaquetes()
        {
            var paquetes = await _context.Paquetes
                .Select(p => new PaqueteDto
                {
                    IdPaquete = p.IdPaquete,
                    NombrePaquete = p.NombrePaquete
                })
                .ToListAsync();

            return Ok(paquetes);
        }
    }
}
