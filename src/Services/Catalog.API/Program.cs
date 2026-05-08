using Catalog.API.Context;
using Catalog.API.Interfaces.Manager;
using Catalog.API.Interfaces.Repository;
using Catalog.API.Manager;
using Catalog.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<CatalogDbContext>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>(provider => provider.GetRequiredService<BookRepository>());
builder.Services.AddScoped<IBookManager, BookManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
