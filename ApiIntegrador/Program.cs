using ApiIntegrador.Data;
using ApiIntegrador.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
// Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
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

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();