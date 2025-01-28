using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;

namespace RestauranteAPI.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(Usuario usuario)
        {
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
           var usuario = await _usuarioRepository.GetByIdAsync(id);
           if (usuario == null)
           {
               //return NotFound();
           }
           await _usuarioRepository.DeleteAsync(id);
           //return NoContent();
        }
        
        public async Task InicializarDatosAsync()
        {
            await _usuarioRepository.InicializarDatosAsync();
        }
        /*
        public async Task AddPlatoPrincipalAsync(PlatoPrincipal platoPrincipal)
        {
            if (platoPrincipal == null)
                throw new ArgumentNullException(nameof(platoPrincipal));

            await _platoPrincipalRepository.AddAsync(platoPrincipal);
        }*/
    }
}


