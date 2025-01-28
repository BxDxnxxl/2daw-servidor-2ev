
using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            var usuarios = new List<Usuario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Apellidos, NombreUsuario FROM usuario";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var usuario = new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellidos = reader.GetString(2),
                                UsuarioNombre = reader.GetString(3),
                            }; 

                            usuarios.Add(usuario);
                            }
                    }
                }
            }
            return usuarios;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            Usuario usuario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Apellidos, NombreUsuario FROM usuario WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellidos = reader.GetString(2),
                                UsuarioNombre = reader.GetString(3),
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public async Task AddAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Usuario (Nombre, Apellidos, NombreUsuario) VALUES (@Nombre, @Apellidos, @UsuarioNombre)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@UsuarioNombre", usuario.UsuarioNombre);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Usuario SET Nombre = @Nombre, Apellidos = @Apellidos, NombreUsuario = @UsuarioNombre WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", usuario.Id);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@UsuarioNombre", usuario.UsuarioNombre);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Usuario WHERE Id = @Id";
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
                    INSERT INTO Usuario (Nombre, Apellidos, NombreUsuario)
                    VALUES 
                    (@Nombre1, @Apellidos1, @UsuarioNombre1),
                    (@Nombre2, @Apellidos2, @UsuarioNombre2)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre1", "Daniel");
                    command.Parameters.AddWithValue("@Apellidos1", "caton perez");
                    command.Parameters.AddWithValue("@UsuarioNombre1", "ByDaniel11");

                    command.Parameters.AddWithValue("@Nombre2", "Izarbe");
                    command.Parameters.AddWithValue("@Apellidos2", "Tolosana Gregorio");
                    command.Parameters.AddWithValue("@UsuarioNombre2", "Zabrux");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}