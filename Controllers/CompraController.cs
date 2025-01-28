using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;
using Models;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _serviceCompra;

        public CompraController(ICompraService service)
        {
            _serviceCompra = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Compra>>> GetCompras()
        {
            var compras = await _serviceCompra.GetAllAsync();
            return Ok(compras);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Compra>> GetCompra(int id)
        {
            var compra = await _serviceCompra.GetByIdAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            return Ok(compra);
        }

        [HttpPost]
        public async Task<ActionResult<Compra>> CreateCompra(Compra compra)
        {
            if (compra == null)
            {
                return BadRequest("Datos de compra inválidos.");
            }
            await _serviceCompra.AddAsync(compra);
            return CreatedAtAction(nameof(GetCompra), new { id = compra.Id }, compra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompra(int id, Compra updateCompra)
        {
            var existingCompra = await _serviceCompra.GetByIdAsync(id);
            if (existingCompra == null)
            {
                return NotFound();
            }

            // Actualizar la compra existente
            existingCompra.FechaCompra = updateCompra.FechaCompra;
            existingCompra.FkIdUsuario = updateCompra.FkIdUsuario;

            await _serviceCompra.UpdateAsync(existingCompra);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            var compra = await _serviceCompra.GetByIdAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            await _serviceCompra.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _serviceCompra.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }
    }
}
