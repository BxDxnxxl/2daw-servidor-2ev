using Models;

namespace RestauranteAPI.Service
{
    public interface ICompraService
    {
        Task<List<Compra>> GetAllAsync();
        Task<Compra?> GetByIdAsync(int id);
        Task AddAsync(Compra compra);
        Task UpdateAsync(Compra compra);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}