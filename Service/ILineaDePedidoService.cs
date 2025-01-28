using Models;

namespace RestauranteAPI.Service
{
    public interface ILineaDePedidoService
    {
        Task<List<LineaDePedido>> GetAllAsync();
        Task<LineaDePedido?> GetByIdAsync(int id);
        Task AddAsync(LineaDePedido lineaDePedido);
        Task UpdateAsync(LineaDePedido lineaDePedido);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
