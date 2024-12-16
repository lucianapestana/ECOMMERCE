using ECOMMERCE.API.DATA.Context;
using ECOMMERCE.API.Repository;
using ECOMMERCE.API.Repository.Interfaces;
using ECOMMERCE.API.Services;
using ECOMMERCE.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados SQL Server
builder.Services.AddDbContext<EcommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração de dependências
builder.Services.AddControllers();

#region [ Services ]

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

#endregion [ Services ]

#region [ Repositories ]

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IFaturamentoRepository, FaturamentoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

#endregion [ Repositories ]

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ECOMMERCE - API",
        Version = "v1",
        Description = "Projeto API para e-commerce"
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Middleware do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECOMMERCE - API v1");
        c.RoutePrefix = string.Empty;  // Swagger disponível em /, não em /swagger
    });
}

app.UseRouting();
app.UseAuthorization();

// Mapear os controllers
app.MapControllers();

app.Run();