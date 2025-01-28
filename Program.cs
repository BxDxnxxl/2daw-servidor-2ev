using RestauranteAPI.Controllers;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RestauranteDB");

builder.Services.AddScoped<IPlatoPrincipalRepository, PlatoPrincipalRepository>(provider =>
new PlatoPrincipalRepository(connectionString));

builder.Services.AddScoped<IPostreRepository, PostreRepository>(provider =>
new PostreRepository(connectionString));

builder.Services.AddScoped<IBebidaRepository, BebidaRepository>(provider =>
new BebidaRepository(connectionString));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(provider =>
new UsuarioRepository(connectionString));

builder.Services.AddScoped<ICompraRepository, CompraRepository>(provider =>
new CompraRepository(connectionString));

builder.Services.AddScoped<ILineaDePedidoRepository, LineaDePedidoRepository>(provider =>
new LineaDePedidoRepository(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add services
builder.Services.AddScoped<IPlatoPrincipalService, PlatoPrincipalService>();
builder.Services.AddScoped<IBebidaService, BebidaService>();
builder.Services.AddScoped<IPostreService, PostreService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<ILineaDePedidoService, LineaDePedidoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



//PlatoPrincipalController.InicializarDatos();
app.Run();
