using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;
using Models;

namespace RestauranteAPI.Service
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;

        public CompraService(ICompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        public async Task<List<Compra>> GetAllAsync()
        {
            return await _compraRepository.GetAllAsync();
        }

        public async Task<Compra?> GetByIdAsync(int id)
        {
            return await _compraRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Compra compra)
        {
            if (compra == null)
                throw new ArgumentNullException(nameof(compra));
            await _compraRepository.AddAsync(compra);
        }

        public async Task UpdateAsync(Compra compra)
        {
            if (compra == null)
                throw new ArgumentNullException(nameof(compra));
            await _compraRepository.UpdateAsync(compra);
        }

        public async Task DeleteAsync(int id)
        {
            var compra = await _compraRepository.GetByIdAsync(id);
            if (compra == null)
            {
                //return NotFound();
            }
            await _compraRepository.DeleteAsync(id);
            //return NoContent();
        }

        public async Task InicializarDatosAsync()
        {
            await _compraRepository.InicializarDatosAsync();
        }
    }
}
