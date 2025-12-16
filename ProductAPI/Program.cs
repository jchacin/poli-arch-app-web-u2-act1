using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models; 
using ProductAPI.Business;
using ProductAPI.Business.Interfaces;
using ProductAPI.Middlewares;
using ProductAPI.Repositories;
using ProductAPI.Repositories.Data; 
using ProductAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios al contenedor.

builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // OpenApiInfo requiere 'using Microsoft.OpenApi.Models;'
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProductAPI",
        Version = "v1",
        Description = "API para gestionar productos"
    });
});

// Inyección de dependencias de tus Servicios y Repositorios
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

// 2. CONFIGURACIÓN DE BASE DE DATOS (Corregido con tu clase real)
// Usamos ApplicationDbContext que es el nombre real en tu archivo
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware de manejo de errores (según tu código original)
app.UseErrorHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductAPI v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();