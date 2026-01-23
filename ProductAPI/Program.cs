using Microsoft.EntityFrameworkCore;
using ProductAPI;
using ProductAPI.Business;
using ProductAPI.Business.Interfaces;
using ProductAPI.Middlewares;
using ProductAPI.Repositories;
using ProductAPI.Repositories.Data; 
using ProductAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Forzar Kestrel a escuchar en 5243 con HTTP/2 (h2) - necesario para gRPC
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5243, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2; // o HttpProtocols.Http1AndHttp2 si necesitas ambos
    });
});

// 1. Agregar servicios al contenedor.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

// Inyección de dependencias de tus Servicios y Repositorios
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

// 2. CONFIGURACIÓN DE BASE DE DATOS (Corregido con tu clase real)
// Usamos ApplicationDbContext que es el nombre real en tu archivo
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseErrorHandler();


if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.MapGrpcService<ProductGrpcService>();

app.Run();