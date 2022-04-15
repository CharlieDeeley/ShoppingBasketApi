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

        public async Task<ConversionRate> GetConversionRate(decimal price, string fromCurrency)
        {
            var response = await _httpClient.GetFromJsonAsync<ConversionRate>(BuildConvertUrl(price, fromCurrency));
            return response;
        }

        private string BuildConvertUrl(decimal price, string fromCurrency)
        {
            var url = "live" + "?access_key=" + Configuration["CurrencyLayerApi:ApiKey"]
                + "&currencies=" + fromCurrency
                + "&to=" + "USD"
                + "&format=" + "1";

            return url;
        }
    }
}
