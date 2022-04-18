using Microsoft.AspNetCore.Mvc;
using ShoppingBasketApi.Services.Abstract;

namespace ShoppingBasketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IShoppingBasketService _shoppingBasketService;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IShoppingBasketService shoppingBasketService, ILogger<BasketController> logger)
        {
            _shoppingBasketService = shoppingBasketService;
            _logger = logger;

        }

        [HttpGet("/GetAvailableItems")]
        public async Task<IActionResult> GetAvailableItems(string currency)
        {
            try
            {
                var items = _shoppingBasketService.GetAvailableItems(currency);

                return Ok(items.Result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/GetBasket")]
        public async Task<IActionResult> GetBasket(string currency)
        {
            try
            {
                var basketItems = _shoppingBasketService.GetBasket(currency);

                if (basketItems.Result.Count() == 0) return Ok("Basket is empty");

                return Ok(basketItems.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("/AddToBasket")]
        public IActionResult AddToBasket(int itemId)
        {
            try
            {
                _shoppingBasketService.AddItem(itemId);

                return Ok("Item added to basket");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("/RemoveFromBasket")]
        public IActionResult RemoveFromBasket(int itemId)
        {
            try
            {
                _shoppingBasketService.RemoveItem(itemId);

                return Ok("Item removed from basket");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
