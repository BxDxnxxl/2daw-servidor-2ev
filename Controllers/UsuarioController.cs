using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UsuarioController : ControllerBase
   {
    private static List<Usuario> usuarios = new List<Usuario>();

    private readonly IUsuarioService _serviceUsuario;

    public UsuarioController(IUsuarioService service)
        {
            _serviceUsuario = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            var usuarios = await _serviceUsuario.GetAllAsync();
            return Ok(usuarios);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _serviceUsuario.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
        {
            await _serviceUsuario.AddAsync(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuario updateUsuario)
        {
            var existingUsuario = await _serviceUsuario.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            // Actualizar el plato existente
            existingUsuario.Nombre = updateUsuario.Nombre;
            existingUsuario.Apellidos = updateUsuario.Apellidos;
            existingUsuario.UsuarioNombre = updateUsuario.UsuarioNombre;

            await _serviceUsuario.UpdateAsync(existingUsuario);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteUsuario(int id)
       {
           var usuario = await _serviceUsuario.GetByIdAsync(id);
           if (usuario == null)
           {
               return NotFound();
           }
           await _serviceUsuario.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _serviceUsuario.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}