using ShoppingBasketApi.Models;
using ShoppingBasketApi.Objects;
using ShoppingBasketApi.Services.Abstract;

namespace ShoppingBasketApi.Services.Concrete
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly IShoppingBasketRepository _shoppingBasketRepository;
        private readonly IPriceConverterService priceConverter;

        private IEnumerable<Item> availableItems;

        public ShoppingBasketService(IPriceConverterService priceConverter, IShoppingBasketRepository shoppingBasketRepository)
        {
            this._shoppingBasketRepository = shoppingBasketRepository;
            this.priceConverter = priceConverter;

            // would be replaced with an ItemsInStock DB table if I had time
            // all references to this would be replaced with calls to dbo.ItemsInStock
            availableItems = new Item[] {
                new Item { Id = 0, Name = "Soup", Price = 0.65m},
                new Item { Id = 1, Name = "Bread", Price = 0.80m},
                new Item { Id = 2, Name = "Milk", Price = 1.15m},
                new Item { Id = 3, Name = "Apples", Price = 1.00m},
            };
        }

        public void AddItem(int id)
        {
            if (IsItemAlreadyInBasket(id))
            {
                throw new ArgumentException("Item is already in basket");
            }

            if (availableItems.Any(x => x.Id == id)) // check if available 
            {
                var item = availableItems.First(x => x.Id == id);
                _shoppingBasketRepository.Add(new Item { Name = item.Name, Price = item.Price});

                _shoppingBasketRepository.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Item not in stock! Please check available items and try again.");
            }
        }

        public void RemoveItem(int id)
        {
            if (_shoppingBasketRepository.Items.Any())
            {
                if (_shoppingBasketRepository.Items.Any(x => x.Id == id))
                {
                    var item = _shoppingBasketRepository.Items.First(x => x.Id == id);
                    _shoppingBasketRepository.Remove(item);

                    _shoppingBasketRepository.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Item not in basket");
                }
            }
            else
            {
                throw new ArgumentException("There are no items in basket to remove");
            }
        }

        public async Task<IEnumerable<Item>> GetAvailableItems(string currency)
        {
            var rate = await priceConverter.GetConversionRate(currency);

            var items = availableItems;

            foreach (var item in items) // adjust prices by conversion rate
            {
                item.Price *= (decimal)rate;
            }

            return items;
        }

        public async Task<IEnumerable<Item>> GetBasket(string currency)
        {
            var rate = await priceConverter.GetConversionRate(currency);

            var items = _shoppingBasketRepository.Items;

            foreach (var item in items) // adjust prices by conversion rate
            {
                item.Price *= (decimal)rate;
            }

            return items;
        }

        private bool IsItemAlreadyInBasket(int id)
        {
            if (_shoppingBasketRepository.Items.Any())
            {
                var item = availableItems.First(x => x.Id == id);
                return _shoppingBasketRepository.Items.Any(x => x.Name == item.Name);
            }

            return false;
        }
    }
}
