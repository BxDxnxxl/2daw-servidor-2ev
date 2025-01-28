using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PostreController : ControllerBase
   {
    private static List<Postre> postres = new List<Postre>();

    private readonly IPostreService _servicePostre;

    public PostreController(IPostreService service)
        {
            _servicePostre = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Postre>>> GetPostres()
        {
            var postres = await _servicePostre.GetAllAsync();
            return Ok(postres);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Postre>> GetPostre(int id)
        {
            var postre = await _servicePostre.GetByIdAsync(id);
            if (postre == null)
            {
                return NotFound();
            }
            return Ok(postre);
        }

        [HttpPost]
        public async Task<ActionResult<Postre>> CreateBebida(Postre postre)
        {
            await _servicePostre.AddAsync(postre);
            return CreatedAtAction(nameof(GetPostre), new { id = postre.Id }, postre);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBebida(int id, Postre updatePostre)
        {
            var existingPostre = await _servicePostre.GetByIdAsync(id);
            if (existingPostre == null)
            {
                return NotFound();
            }

            // Actualizar el plato existente
            existingPostre.Nombre = updatePostre.Nombre;
            existingPostre.Precio = updatePostre.Precio;
            existingPostre.Calorias = updatePostre.Calorias;

            await _servicePostre.UpdateAsync(existingPostre);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeletePlato(int id)
       {
           var postre = await _servicePostre.GetByIdAsync(id);
           if (postre == null)
           {
               return NotFound();
           }
           await _servicePostre.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _servicePostre.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}