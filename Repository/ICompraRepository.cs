using Models;

namespace RestauranteAPI.Repositories
{
    public interface ICompraRepository
    {
        Task<List<Compra>> GetAllAsync();
        Task<Compra?> GetByIdAsync(int id);
        Task AddAsync(Compra compra);
        Task UpdateAsync(Compra compra);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
