using SalePayment.Infrastructure.Repositories;
using SalePayment.Domain.Repositories;
using SalePayment.Infrastructure.Data;
using SalePayment.Application.Handlers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using SalePayment.Application.Mapper;
using SalePayment.Application.Producer.RabbitMQ;
using SalePayment.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<SalePaymentContext>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IMessageProducer, RabbitMQProducer>();

builder.Services.AddDbContext<SalePaymentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(GetInvoicesHandler).Assembly);
});
builder.Services.AddGrpcClient<GetProductInfo.GetProductInfoClient>(o =>
{
    o.Address = new Uri("https://localhost:5000");
});

//disable automatic 400 response
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
options.SuppressModelStateInvalidFilter = true) ;

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseMiddleware<AutomatedRequestMiddleware>();
app.MapControllers();

app.Run();
