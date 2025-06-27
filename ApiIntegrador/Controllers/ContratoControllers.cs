using ApiIntegrador.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiIntegrador.Services;

namespace ApiIntegrador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratoController : ControllerBase
    {
        private readonly ContratoService _contratoService;

        public ContratoController(ContratoService contratoService)
        {
            _contratoService = contratoService;
        }

        [HttpGet("{idSuscriptor}/deuda")]
        public async Task<ActionResult<SuscriptorDeudaDTO>> ObtenerDeuda(int idSuscriptor)
        {
            var resultado = await _contratoService.CalcularDeudaTotalAsync(idSuscriptor);
            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
    }
}