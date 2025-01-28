using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class BebidaController : ControllerBase
   {
    private static List<Bebida> bebidas = new List<Bebida>();

    private readonly IBebidaService _serviceBebida;

    public BebidaController(IBebidaService service)
        {
            _serviceBebida = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Bebida>>> GetBebidas()
        {
            var bebidas = await _serviceBebida.GetAllAsync();
            return Ok(bebidas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Bebida>> GetBebida(int id)
        {
            var bebida = await _serviceBebida.GetByIdAsync(id);
            if (bebida == null)
            {
                return NotFound();
            }
            return Ok(bebida);
        }

        [HttpPost]
        public async Task<ActionResult<PlatoPrincipal>> CreateBebida(Bebida bebida)
        {
            await _serviceBebida.AddAsync(bebida);
            return CreatedAtAction(nameof(GetBebida), new { id = bebida.Id }, bebida);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBebida(int id, Bebida updateBebida)
        {
            var existingBebida = await _serviceBebida.GetByIdAsync(id);
            if (existingBebida == null)
            {
                return NotFound();
            }

            // Actualizar el plato existente
            existingBebida.Nombre = updateBebida.Nombre;
            existingBebida.Precio = updateBebida.Precio;
            existingBebida.EsAlcoholica = updateBebida.EsAlcoholica;

            await _serviceBebida.UpdateAsync(existingBebida);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeletePlato(int id)
       {
           var bebida = await _serviceBebida.GetByIdAsync(id);
           if (bebida == null)
           {
               return NotFound();
           }
           await _serviceBebida.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _serviceBebida.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}