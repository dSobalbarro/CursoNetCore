using BibliotecaApi;
using BibliotecaApi.Datos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//areá de servicios

builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

//Cuando alguien ocupe el servicio que le proporcioné la instancia RepositorioValores
builder.Services.AddSingleton<IRepositorioValores, RepositorioValoresOracle>(); 

builder.Services.AddControllers().AddJsonOptions(opciones =>
opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDBContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();
// areá de midlewares

app.MapControllers();

app.Run();
