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

        [HttpGet("/GetAvailableItems")]
        public async Task<IActionResult> GetAvailableItems(string currency)
        {
            var rate = await priceConverter.GetConversionRate(currency);

            return Ok("");
        }

        [HttpGet("/GetBasket")]
        public async Task<IActionResult> GetBasket(int userId, string currency)
        {
            // call db and get basket for user id
            return Ok("");
        }

        [HttpPatch("/AddToBasket")]
        public async Task<IActionResult> AddToBasket(int userId, int itemId)
        {
            return Ok("");
        }

        [HttpPatch("/RemoveFromBasket")]
        public async Task<IActionResult> RemoveFromBasket(int id, string itemId)
        {
            return Ok("");
        }
    }
}
