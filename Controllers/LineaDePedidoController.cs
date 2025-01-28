using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineaDePedidoController : ControllerBase
    {
        private static List<LineaDePedido> lineasDePedido = new List<LineaDePedido>();

        private readonly ILineaDePedidoService _serviceLineaDePedido;

        public LineaDePedidoController(ILineaDePedidoService service)
        {
            _serviceLineaDePedido = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<LineaDePedido>>> GetLineasDePedido()
        {
            var lineas = await _serviceLineaDePedido.GetAllAsync();
            return Ok(lineas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LineaDePedido>> GetLineaDePedido(int id)
        {
            var lineaDePedido = await _serviceLineaDePedido.GetByIdAsync(id);
            if (lineaDePedido == null)
            {
                return NotFound();
            }
            return Ok(lineaDePedido);
        }

        [HttpPost]
        public async Task<ActionResult<LineaDePedido>> CreateLineaDePedido(LineaDePedido lineaDePedido)
        {
            await _serviceLineaDePedido.AddAsync(lineaDePedido);
            return CreatedAtAction(nameof(GetLineaDePedido), new { id = lineaDePedido.Id }, lineaDePedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLineaDePedido(int id, LineaDePedido updateLineaDePedido)
        {
            var existingLineaDePedido = await _serviceLineaDePedido.GetByIdAsync(id);
            if (existingLineaDePedido == null)
            {
                return NotFound();
            }

            existingLineaDePedido.FkIdCompra = updateLineaDePedido.FkIdCompra;
            existingLineaDePedido.PlatoPrincipalId = updateLineaDePedido.PlatoPrincipalId;
            existingLineaDePedido.PostreId = updateLineaDePedido.PostreId;
            existingLineaDePedido.BebidaId = updateLineaDePedido.BebidaId;
            existingLineaDePedido.Cantidad = updateLineaDePedido.Cantidad;

            await _serviceLineaDePedido.UpdateAsync(existingLineaDePedido);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineaDePedido(int id)
        {
            var lineaDePedido = await _serviceLineaDePedido.GetByIdAsync(id);
            if (lineaDePedido == null)
            {
                return NotFound();
            }
            await _serviceLineaDePedido.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{idUser}/lineasdepedido")]
        public async Task<ActionResult<LienaDePedidoDTO>> GetPedidoByUser(int idUser)
        {
            var lineaDePedido = await _serviceLineaDePedido.GetPedidosByUserAsync(idUser);
            if (lineaDePedido == null)
            {
                return NotFound();
            }
            return Ok(lineaDePedido);
        }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _serviceLineaDePedido.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }
    }
}
