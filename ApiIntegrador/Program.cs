using ApiIntegrador.Data;
using ApiIntegrador.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS


// Configurar cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar servicios personalizados
builder.Services.AddScoped<ContratoService>();
builder.Services.AddScoped<PromocionService>();

// Agregar controladores
builder.Services.AddControllers();

// Agregar CORS (ejemplo básico, opcional)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // URL de tu app Angular (dev)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiIntegrador v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();