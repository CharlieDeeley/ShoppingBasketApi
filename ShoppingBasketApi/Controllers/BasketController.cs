using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingBasketApi.Models.Abstract;
using ShoppingBasketApi.Models.Concrete;
using ShoppingBasketApi.Services.Abstract;

namespace ShoppingBasketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IPriceConverterService priceConverter;

        private readonly ILogger<BasketController> _logger;

        private IShoppingBasket shoppingBasket;

        public BasketController(IPriceConverterService priceConverter, ILogger<BasketController> logger)
        {
            this.priceConverter = priceConverter;
            _logger = logger;

            shoppingBasket = new ShoppingBasket();
        }

        [HttpPost("/CreateBasket")]
        public IActionResult CreateBasket()
        {
            try
            {
                var id = shoppingBasket.CreateBasket();

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/GetAvailableItems")]
        public async Task<IActionResult> GetAvailableItems(string currency)
        {
            var rate = await priceConverter.GetConversionRate(currency);

            var items = shoppingBasket.GetAvailableItems(rate);

            return Ok(items);
        }

        [HttpGet("/GetBasket")]
        public async Task<IActionResult> GetBasket(int basketId, string currency)
        {
            var rate = await priceConverter.GetConversionRate(currency);

            try
            {
                var basketItems = shoppingBasket.GetBasket(basketId, rate);

                return Ok(basketItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPatch("/AddToBasket")]
        public IActionResult AddToBasket(int basketId, int itemId)
        {
            try
            {
                shoppingBasket.AddToBasket(basketId, itemId);

                return Ok("Item added to basket");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("/RemoveFromBasket")]
        public IActionResult RemoveFromBasket(int basketId, int itemId)
        {
            try
            {
                shoppingBasket.AddToBasket(basketId, itemId);

                return Ok("Item removed from basket");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
