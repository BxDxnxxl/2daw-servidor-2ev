using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestauranteAPI.Repositories
{
    public class CompraRepository : ICompraRepository
    {
        private readonly string _connectionString;

        public CompraRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Compra>> GetAllAsync()
        {
            var compras = new List<Compra>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, FkIdUsuario, FechaCompra FROM compra";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var compra = new Compra
                            {
                                Id = reader.GetInt32(0),
                                FkIdUsuario = reader.GetInt32(1),
                                FechaCompra = reader.GetDateTime(2),
                            };

                            compras.Add(compra);
                        }
                    }
                }
            }
            return compras;
        }

        public async Task<Compra> GetByIdAsync(int id)
        {
            Compra compra = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, FkIdUsuario, FechaCompra FROM compra WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            compra = new Compra
                            {
                                Id = reader.GetInt32(0),
                                FkIdUsuario = reader.GetInt32(1),
                                FechaCompra = reader.GetDateTime(2),
                            };
                        }
                    }
                }
            }
            return compra;
        }

        public async Task AddAsync(Compra compra)
        {
            DateTime fecha = DateTime.Now;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO compra (FkIdUsuario, FechaCompra) VALUES (@FkIdUsuario, @FechaCompra)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FkIdUsuario", compra.FkIdUsuario);
                    command.Parameters.AddWithValue("@FechaCompra", fecha);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Compra compra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE compra SET FkIdUsuario = @FkIdUsuario, FechaCompra = @FechaCompra WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", compra.Id);
                    command.Parameters.AddWithValue("@FkIdUsuario", compra.FkIdUsuario);
                    command.Parameters.AddWithValue("@FechaCompra", compra.FechaCompra);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM compra WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                    INSERT INTO compra (FkIdUsuario, FechaCompra)
                    VALUES 
                    (@FkIdUsuario1, @FechaCompra1),
                    (@FkIdUsuario2, @FechaCompra2)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FkIdUsuario1", 1);
                    command.Parameters.AddWithValue("@FechaCompra1", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@FkIdUsuario2", 2);
                    command.Parameters.AddWithValue("@FechaCompra2", DateTime.UtcNow);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
    
}
