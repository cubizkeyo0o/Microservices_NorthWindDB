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
using Microsoft.AspNetCore.Authentication.Cookies;

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
//-----------Authen & Atuthor-----------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "bearer";
    options.DefaultChallengeScheme = "bearer";
}).AddJwtBearer("bearer", options =>
{
    options.Authority = "https://localhost:5001";
    options.TokenValidationParameters.ValidateAudience = true;
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();