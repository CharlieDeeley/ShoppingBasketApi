using ShoppingBasketApi.Models.Abstract;
using ShoppingBasketApi.Objects;

namespace ShoppingBasketApi.Models.Concrete
{
    public class ShoppingBasket : IShoppingBasket
    {
        private Dictionary<int, List<BasketItem>> shoppingBaskets;
        private IEnumerable<BasketItem> availableItems;

        public ShoppingBasket()
        {
            shoppingBaskets = new Dictionary<int, List<BasketItem>>();

            availableItems = new BasketItem[] {
                new BasketItem { Id = 0, Name = "Soup", Price = 0.65m},
                new BasketItem { Id = 1, Name = "Bread", Price = 0.80m},
                new BasketItem { Id = 2, Name = "Milk", Price = 1.15m},
                new BasketItem { Id = 3, Name = "Apples", Price = 1.00m},
            };
        }

        public int CreateBasket()
        {
            var id = shoppingBaskets.Count + 1;

            shoppingBaskets.Add(id, new List<BasketItem>());

            return id;
        }

        public IEnumerable<BasketItem> GetAvailableItems(double rate)
        {
            var items = availableItems;

            foreach (var item in items) // adjust prices by conversion rate
            {
                item.Price *= (decimal)rate;
            }

            return items;
        }

        public void AddToBasket(int basketId, int itemId)
        {
            var userBasket = shoppingBaskets[basketId];

            if (userBasket != null)
            {
                var item = availableItems.First(x => x.Id == itemId);

                if (item != null)
                {
                    userBasket.Add(item);
                }
                else
                {
                    throw new ArgumentException("Item does not exist");
                }
            }
            else
            {
                throw new ArgumentException("Basket does not exist");
            }
        }

        public void RemoveFromBasket(int basketId, int itemId)
        {
            var userBasket = shoppingBaskets[basketId];

            if (userBasket != null)
            {
                if (!userBasket.Remove(availableItems.First(x => x.Id == itemId)))
                {
                    throw new ArgumentException("Item does not exist");
                }
            }
            else
            {
                throw new ArgumentException("Basket does not exist");
            }
        }

        public IEnumerable<BasketItem> GetBasket(int basketId, double rate)
        {
            var userBasket = shoppingBaskets[basketId];
            if (userBasket != null)
            {
                var basketItems = userBasket;

                foreach (var item in basketItems) // adjust prices by conversion rate
                {
                    item.Price *= (decimal)rate;
                }

                return basketItems;
            }
            else
            {
                throw new ArgumentException("Basket does not exist");
            }
        }
    }
}
