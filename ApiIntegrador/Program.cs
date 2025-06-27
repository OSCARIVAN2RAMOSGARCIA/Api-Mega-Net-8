using ApiIntegrador.Data; // cambia por el namespace correcto
using Microsoft.Extensions.DependencyInjection;
using ApiIntegrador.Controllers;
using ApiIntegrador.Dto;
using System.Reflection;
using AutoMapper;
using ApiIntegrador.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar cadena de conexión (ajústala a tu entorno)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Agregar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar servicios personalizados
builder.Services.AddScoped<ContratoService>();

// Agregar controladores
builder.Services.AddControllers();

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de seguridad y ruteo
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
