using appviacet.Services.External;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appviacet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaCepController : ControllerBase
    {
        private readonly ViaCepService _viaCEPService;

        public ConsultaCepController(ViaCepService viaCepService)
        {
            _viaCEPService = viaCepService;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> ConsultarCEP(string cep)
        {
            var resultado = await _viaCEPService.ConsultarCEPAsync(cep);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }
    }
}
