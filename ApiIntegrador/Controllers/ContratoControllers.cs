using ApiIntegrador.Dto;
using ApiIntegrador.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIntegrador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratoController : ControllerBase
    {
        private readonly ContratoService _service;

        public ContratoController(ContratoService service)
        {
            _service = service;
        }

        // GET api/contrato/{idSuscriptor}/pago
        [HttpGet("{idSuscriptor}/pago")]
        public async Task<ActionResult<SuscriptorPagoDTO>> GetPagoMensual(int idSuscriptor)
        {
            var pago = await _service.CalcularPagoMensualAsync(idSuscriptor);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        // GET api/contrato/pagos
        [HttpGet("pagos")]
        public async Task<ActionResult<List<SuscriptorPagoDTO>>> GetPagosMensuales()
        {
            var pagos = await _service.CalcularPagosMensualesAsync();
            return Ok(pagos);
        }
    }
}
