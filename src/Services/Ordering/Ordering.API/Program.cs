using Microsoft.EntityFrameworkCore;
using Ordering.API.EventBusConsumer;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<OrderContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Ordering.API")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketCheckoutConsumer>();

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {
    config.AddConsumer<BasketCheckoutConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));

        cfg.ReceiveEndpoint("basketcheckout-queue", c => {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });
    });
});

var app = builder.Build();

// Ensure Database is Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OrderContext>();
    try
    {
        context.Database.EnsureCreated();
    }
    catch (Exception)
    {
        // Log error
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
