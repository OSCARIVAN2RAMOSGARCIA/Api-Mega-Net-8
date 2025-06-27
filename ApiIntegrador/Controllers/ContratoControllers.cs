using ApiIntegrador.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        // Obtener pago mensual de un suscriptor específico
        [HttpGet("{idSuscriptor}/pago")]
        public async Task<ActionResult<SuscriptorPagoDTO>> ObtenerPago(int idSuscriptor)
        {
            var resultado = await _contratoService.CalcularPagoMensualAsync(idSuscriptor);
            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        // Obtener pagos de todos los suscriptores
        [HttpGet("pagos")]
        public async Task<ActionResult<List<SuscriptorPagoDTO>>> ObtenerTodosLosPagos()
        {
            var lista = await _contratoService.CalcularPagosMensualesAsync();
            return Ok(lista);
        }

        // ✅ Obtener deuda total de un suscriptor (nuevo endpoint)
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
