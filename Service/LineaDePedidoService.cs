using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;

namespace RestauranteAPI.Service
{
    public class LineaDePedidoService : ILineaDePedidoService
    {
        private readonly ILineaDePedidoRepository _lineaDePedidoRepository;

        public LineaDePedidoService(ILineaDePedidoRepository lineaDePedidoRepository)
        {
            _lineaDePedidoRepository = lineaDePedidoRepository;
        }

        public async Task<List<LineaDePedido>> GetAllAsync()
        {
            return await _lineaDePedidoRepository.GetAllAsync();
        }

        public async Task<LineaDePedido?> GetByIdAsync(int id)
        {
            return await _lineaDePedidoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(LineaDePedido lineaDePedido)
        {
            await _lineaDePedidoRepository.AddAsync(lineaDePedido);
        }

        public async Task UpdateAsync(LineaDePedido lineaDePedido)
        {
            await _lineaDePedidoRepository.UpdateAsync(lineaDePedido);
        }

        public async Task DeleteAsync(int id)
        {
            var lineaDePedido = await _lineaDePedidoRepository.GetByIdAsync(id);
            if (lineaDePedido == null)
            {
                //excepcion a implementar
            }
            await _lineaDePedidoRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync()
        {
            await _lineaDePedidoRepository.InicializarDatosAsync();
        }
    }
}
