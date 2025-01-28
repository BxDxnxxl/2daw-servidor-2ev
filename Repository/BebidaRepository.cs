
using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{
    public class BebidaRepository : IBebidaRepository
    {
        private readonly string _connectionString;

        public BebidaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Bebida>> GetAllAsync()
        {
            var bebidas = new List<Bebida>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, esAlcoholica FROM Bebida";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var bebida = new Bebida
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = (double)reader.GetDecimal(2),
                                EsAlcoholica = reader.GetByte(3) == 0 ? false : true
                            }; 

                            bebidas.Add(bebida);
                            }
                    }
                }
            }
            return bebidas;
        }

        public async Task<Bebida> GetByIdAsync(int id)
        {
            Bebida bebida = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, esAlcoholica FROM Bebida WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            bebida = new Bebida
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = (double) reader.GetDecimal(2),
                                EsAlcoholica = reader.GetByte(3) == 0 ? false : true
                            };
                        }
                    }
                }
            }
            return bebida;
        }

        public async Task AddAsync(Bebida bebida)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Bebida (Nombre, Precio, esAlcoholica) VALUES (@Nombre, @Precio, @esAlcoholica)";
                using (var command = new SqlCommand(query, connection))
                {
                    var alcoholica = bebida.EsAlcoholica ? 1 : 0;
                    command.Parameters.AddWithValue("@Nombre", bebida.Nombre);
                    command.Parameters.AddWithValue("@Precio", bebida.Precio);
                    command.Parameters.AddWithValue("@esAlcoholica", alcoholica);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Bebida bebida)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Bebida SET Nombre = @Nombre, Precio = @Precio, esAlcoholica = @esAlcoholica WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", bebida.Id);
                    command.Parameters.AddWithValue("@Nombre", bebida.Nombre);
                    command.Parameters.AddWithValue("@Precio", bebida.Precio);
                    command.Parameters.AddWithValue("@esAlcoholica", bebida.EsAlcoholica);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Bebida WHERE Id = @Id";
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

                // Comando SQL para insertar datos iniciales
                var query = @"
                    INSERT INTO Bebida (Nombre, Precio, esAlcoholica)
                    VALUES 
                    (@Nombre1, @Precio1, @esAlcoholica),
                    (@Nombre2, @Precio2, @esAlcoholica2)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre1", "RonCola");
                    command.Parameters.AddWithValue("@Precio1", 5);
                    command.Parameters.AddWithValue("@esAlcoholica",1);

                    command.Parameters.AddWithValue("@Nombre2", "Cocacola");
                    command.Parameters.AddWithValue("@Precio2", 2.70);
                    command.Parameters.AddWithValue("@esAlcoholica2", 0);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}