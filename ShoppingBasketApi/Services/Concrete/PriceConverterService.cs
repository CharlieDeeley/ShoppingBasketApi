using ShoppingBasketApi.Objects.Responses;
using ShoppingBasketApi.Services.Abstract;

namespace ShoppingBasketApi.Services.Concrete
{
    public class PriceConverterService : IPriceConverterService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration Configuration;
        public PriceConverterService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("converter");
            Configuration = configuration;
        }

        public async Task<double> GetConversionRate(string fromCurrency)
        {
            var response = await _httpClient.GetFromJsonAsync<ConversionRate>(BuildConvertUrl(fromCurrency));
            return response.Quotes["USD" + fromCurrency];
        }

        private string BuildConvertUrl(string fromCurrency)
        {
            return "live" + "?access_key=" + Configuration["CurrencyLayerApi:ApiKey"]
                + "&currencies=" + fromCurrency
                + "&to=" + "USD"
                + "&format=" + "1";
        }
    }
}
