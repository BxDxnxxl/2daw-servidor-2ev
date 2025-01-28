using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;

namespace RestauranteAPI.Service
{
    public class PlatoPrincipalService : IPlatoPrincipalService
    {
        private readonly IPlatoPrincipalRepository _platoPrincipalRepository;

        public PlatoPrincipalService(IPlatoPrincipalRepository platoPrincipalRepository)
        {
            _platoPrincipalRepository = platoPrincipalRepository;
        }

        public async Task<List<PlatoPrincipal>> GetAllAsync()
        {
            return await _platoPrincipalRepository.GetAllAsync();
        }

        public async Task<PlatoPrincipal?> GetByIdAsync(int id)
        {
            return await _platoPrincipalRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(PlatoPrincipal platoPrincipal)
        {
            await _platoPrincipalRepository.AddAsync(platoPrincipal);
        }

        public async Task UpdateAsync(PlatoPrincipal platoPrincipal)
        {
            await _platoPrincipalRepository.UpdateAsync(platoPrincipal);
        }

        public async Task DeleteAsync(int id)
        {
           var plato = await _platoPrincipalRepository.GetByIdAsync(id);
           if (plato == null)
           {
               //return NotFound();
           }
           await _platoPrincipalRepository.DeleteAsync(id);
           //return NoContent();
        }
        
        public async Task InicializarDatosAsync()
        {
            await _platoPrincipalRepository.InicializarDatosAsync();
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


