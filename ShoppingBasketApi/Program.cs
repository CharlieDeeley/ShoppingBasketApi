using Microsoft.EntityFrameworkCore;
using ShoppingBasketApi.Models;
using ShoppingBasketApi.Services.Abstract;
using ShoppingBasketApi.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add in memory dbcontext
builder.Services.AddDbContext<ShoppingBasketContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddTransient<IShoppingBasketRepository, DbRepository>();

// add http client for calls to CurrencyLayerApi 
builder.Services.AddHttpClient("converter",
    client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["CurrencyLayerApi:BaseUrl"]);
    });

// Add Services
builder.Services.AddScoped<IPriceConverterService, PriceConverterService>();
builder.Services.AddScoped<IShoppingBasketService, ShoppingBasketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
