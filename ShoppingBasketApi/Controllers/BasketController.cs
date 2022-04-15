using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingBasketApi.Services.Abstract;

namespace ShoppingBasketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IPriceConverterService priceConverter;

        private readonly ILogger<BasketController> _logger;

        public BasketController(IPriceConverterService priceConverter, ILogger<BasketController> logger)
        {
            this.priceConverter = priceConverter;
            _logger = logger;
        }

        [HttpGet(Name = "GetAvailableItems")]
        public async Task<IActionResult> GetAvailableItems(string currency)
        {
            var conversionRate = await priceConverter.GetConversionRate(0, currency);
            var rate = conversionRate.Quotes["USD" + currency];

            return Ok("");
        }

        [HttpGet(Name = "GetUserShoppingBasket")]
        public async Task<IActionResult> GetUserShoppingBasket(int id, string currency)
        {
            // call db and get basket for user id
            return Ok("");
        }
    }
}
