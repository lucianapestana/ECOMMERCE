using System.Net.Http.Headers;
using ECOMMERCE.API.DATA.Context;
using ECOMMERCE.API.Repository;
using ECOMMERCE.API.Repository.Interfaces;
using ECOMMERCE.API.Services;
using ECOMMERCE.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EcommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddHttpClient<FaturamentoService>(client =>
{
    client.BaseAddress = new Uri("https://sti3-faturamento.azurewebsites.net");
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

#region [ Services ]

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IFaturamentoService, FaturamentoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

#endregion [ Services ]

#region [ Repositories ]

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IFaturamentoRepository, FaturamentoRepository>();
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

#endregion [ Repositories ]

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

app.MapControllers();

app.Run();