using Models;

namespace RestauranteAPI.Repositories
{
    public interface ILineaDePedidoRepository
    {
        Task<List<LineaDePedido>> GetAllAsync();
        Task<LineaDePedido?> GetByIdAsync(int id);
        Task AddAsync(LineaDePedido lineaPedido);
        Task UpdateAsync(LineaDePedido Compra);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
