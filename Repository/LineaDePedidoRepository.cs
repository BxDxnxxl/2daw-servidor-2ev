using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace RestauranteAPI.Repositories
{
    public class LineaDePedidoRepository : ILineaDePedidoRepository
    {
        private readonly string _connectionString;

        public LineaDePedidoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<LineaDePedido>> GetAllAsync()
        {
            var lineasDePedido = new List<LineaDePedido>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, FkIdCompra, PlatoPrincipalId, PostreId, BebidaId, Cantidad FROM LineaDePedido";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var lineaDePedido = new LineaDePedido{
                                Id = reader.GetInt32(0),
                                FkIdCompra = reader.GetInt32(1),
                                PlatoPrincipalId = reader.GetInt32(2),
                                PostreId = reader.GetInt32(3), 
                                BebidaId = reader.GetInt32(4), 
                                Cantidad = reader.GetInt32(5)
                            };

                            lineasDePedido.Add(lineaDePedido);
                        }
                    }
                }
            }
            return lineasDePedido;
        }

        public async Task<LineaDePedido> GetByIdAsync(int id)
        {
            LineaDePedido lineaDePedido = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, FkIdCompra, PlatoPrincipalId, PostreId, BebidaId, Cantidad FROM LineaDePedido WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            lineaDePedido = new LineaDePedido{
                                Id = reader.GetInt32(0),
                                FkIdCompra = reader.GetInt32(1),
                                PlatoPrincipalId = reader.GetInt32(2),
                                PostreId = reader.GetInt32(3), 
                                BebidaId = reader.GetInt32(4), 
                                Cantidad = reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return lineaDePedido;
        }

        public async Task AddAsync(LineaDePedido lineaDePedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO LineaDePedido (FkIdCompra, PlatoPrincipalId, PostreId, BebidaId, Cantidad) " +
                               "VALUES (@FkIdCompra, @PlatoPrincipalId, @PostreId, @BebidaId, @Cantidad)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FkIdCompra", lineaDePedido.FkIdCompra);
                    command.Parameters.AddWithValue("@PlatoPrincipalId", lineaDePedido.PlatoPrincipalId);
                    command.Parameters.AddWithValue("@PostreId", lineaDePedido.PostreId);
                    command.Parameters.AddWithValue("@BebidaId", lineaDePedido.BebidaId);
                    command.Parameters.AddWithValue("@Cantidad", lineaDePedido.Cantidad);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(LineaDePedido lineaDePedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE LineaDePedido SET FkIdCompra = @FkIdCompra, PlatoPrincipalId = @PlatoPrincipalId, " +
                               "PostreId = @PostreId, BebidaId = @BebidaId, Cantidad = @Cantidad " +
                               "WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", lineaDePedido.Id);
                    command.Parameters.AddWithValue("@FkIdCompra", lineaDePedido.FkIdCompra);
                    command.Parameters.AddWithValue("@PlatoPrincipalId", lineaDePedido.PlatoPrincipalId);
                    command.Parameters.AddWithValue("@PostreId", lineaDePedido.PostreId);
                    command.Parameters.AddWithValue("@BebidaId", lineaDePedido.BebidaId);
                    command.Parameters.AddWithValue("@Cantidad", lineaDePedido.Cantidad);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM LineaDePedido WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<LienaDePedidoDTO>> GetPedidosByUserAsync(int idUser){
            var lineasDePedido = new List<LienaDePedidoDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT PP.nombre, B.Nombre, P.nombre FROM LineaDePedido as LDP " +
                 "inner join compra as C on C.id = LDP.FkIdCompra "+
                 "inner join platoprincipal as PP on PP.id = LDP.PlatoPrincipalId "+
                 "inner join bebida as B on B.id = LDP.BebidaId "+
                 "inner join postre as P on P.id = LDP.PostreId "+
                 "WHERE C.FkIdUsuario = ";
                 query = query + idUser;

                using (var command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@userId", idUser);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var lineaDePedido = new LienaDePedidoDTO{
                                PlatoPrincipalNombre = reader.GetString(0),
                                BebidaNombre = reader.GetString(1),
                                PostreNombre = reader.GetString(2)
                            };

                            lineasDePedido.Add(lineaDePedido);
                        }
                    }
                }
            }
            return lineasDePedido;
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Comando SQL para insertar datos iniciales
                var query = @"
                    INSERT INTO LineaDePedido (FkIdCompra, PlatoPrincipalId, PostreId, BebidaId, Cantidad)
                    VALUES 
                    (@FkIdCompra1, @PlatoPrincipalId1, @PostreId1, @BebidaId1, @Cantidad1),
                    (@FkIdCompra2, @PlatoPrincipalId2, @PostreId2, @BebidaId2, @Cantidad2)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FkIdCompra1", 3);
                    command.Parameters.AddWithValue("@PlatoPrincipalId1", 1);
                    command.Parameters.AddWithValue("@PostreId1", 1);
                    command.Parameters.AddWithValue("@BebidaId1", 1);
                    command.Parameters.AddWithValue("@Cantidad1", 2);

                    command.Parameters.AddWithValue("@FkIdCompra2", 2);
                    command.Parameters.AddWithValue("@PlatoPrincipalId2", 2 );
                    command.Parameters.AddWithValue("@PostreId2", 2);
                    command.Parameters.AddWithValue("@BebidaId2", 2);
                    command.Parameters.AddWithValue("@Cantidad2", 1);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
