using Customer.Application.Consumer.RabbitMQ;
using Customer.Application.Emails;
using Customer.Application.Mapper;
using Microsoft.AspNetCore.Mvc;
using Customer.Infrastructure;
using Customer.Domain.Repositories;
using Customer.Infrastructure.Repositories;
using Customer.Infrastructure.Data;
using Customer.Application.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IMessageConsumer, RabbitMQConsumer2>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(GetCustomerByIdHandler).Assembly);
});
builder.Services.AddDbContext<CustomerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));


//config masstransit.rabbitmq 
//builder.Services.AddScoped<InvoiceCheckoutConsumer>();
//builder.Services.AddMassTransit(config =>
//{
//    //Mark this as consumer
//    config.AddConsumer<InvoiceCheckoutConsumer>();

//    config.UsingRabbitMq((ctx, cfg) =>
//    {
//        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
//        //provide the queue name with consumer settings
//        cfg.ReceiveEndpoint(EventBusContracts.InvoiceCheckoutQueue, c =>
//        {
//            c.ConfigureConsumer<InvoiceCheckoutConsumer>(ctx);
//        });
//    });
//});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();