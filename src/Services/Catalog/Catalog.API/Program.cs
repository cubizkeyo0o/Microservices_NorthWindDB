using Catalog.API.Controllers.ExerciseDependencyInjection;
using Catalog.Application.Handlers;
using Catalog.Application.Mapper;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Repositories;
using Catalog.API.GrpcService;
using MediatR;
using Catalog.Domain.Entities;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IClassA, ClassA>();
builder.Services.AddSingleton<ClassB>();
builder.Services.AddSingleton<ProductGrpcService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<CatalogContext, CatalogContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ISupplierRepository, ProductRepository>();

builder.Services.AddDbContext<CatalogContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(GetAllProductsHandle).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(GetProductGrpcHandle).Assembly);
});
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<ProductService>();
app.Run();
