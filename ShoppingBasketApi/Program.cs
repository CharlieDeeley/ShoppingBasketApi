using ShoppingBasketApi.Services.Abstract;
using ShoppingBasketApi.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add http client for calls to CurrencyLayerApi 
builder.Services.AddHttpClient("converter",
    client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["CurrencyLayerApi:BaseUrl"]);
    });

// Add Services
builder.Services.AddScoped<IPriceConverterService, PriceConverterService>();

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
