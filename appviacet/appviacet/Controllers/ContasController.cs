using appviacet.Context.Entitys;
using appviacet.Services.Internal;
using appviacet.Services.Internal.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace appviacet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        private readonly IContasService _contasService;

        public ContasController(IContasService contasService)
        {
            _contasService = contasService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conta>>> GetContas()
        {
            var contas = await _contasService.GetContasAsync();
            return Ok(contas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Conta>> GetConta(int id)
        {
            var conta = await _contasService.GetContaAsync(id);
            if (conta == null)
            {
                return NotFound();
            }
            return Ok(conta);
        }

        [HttpPost]
        public async Task<ActionResult<Conta>> PostConta([FromBody] Conta conta)
        {
            var createdConta = await _contasService.CreateContaAsync(conta);
            return CreatedAtAction(nameof(GetConta), new { id = createdConta.Id }, createdConta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutConta(int id, Conta conta)
        {
            try
            {
                var updatedConta = await _contasService.UpdateContaAsync(id, conta);
                return Ok(updatedConta);
            }
            catch (ArgumentException)
            {
                return BadRequest("Id nao encontrado");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConta(int id)
        {
            var result = await _contasService.DeleteContaAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
