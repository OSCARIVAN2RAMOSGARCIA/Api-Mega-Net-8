using Microsoft.EntityFrameworkCore;
using ApiIntegrador.Data; // cambia por el namespace correcto
using Microsoft.Extensions.DependencyInjection;
using ApiIntegrador.Controllers;
using ApiIntegrador.Dto;
using System.Reflection;
using AutoMapper;
using ApiIntegrador.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ContratoService>();
builder.Services.AddScoped<PromocionService>();
// AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


// Swagger
builder.Services.AddSwaggerGen();
// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // Solo muestra Swagger en desarrollo
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// swagger 
// http://localhost:5243/swagger/index.html

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Aplicar migraciones automáticamente (solo para desarrollo)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al migrar la base de datos.");
    }
}

app.Run();