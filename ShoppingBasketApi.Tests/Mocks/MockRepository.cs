using ShoppingBasketApi.Models;
using ShoppingBasketApi.Objects;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasketApi.Tests.Mocks
{
    internal class MockRepository : IShoppingBasketRepository
    {
        private readonly List<Item> _items = new List<Item>();
        private readonly List<ShoppingBasket> _shoppingBaskets = new List<ShoppingBasket>();

        public IQueryable<Item> Items => _items.AsQueryable();
        public IQueryable<ShoppingBasket> ShoppingBasket => _shoppingBaskets.AsQueryable();

        public void Add<EntityType>(EntityType entity)
        {
            switch (entity)
            {
                case Item book:
                    _items.Add(book);
                    break;
                case ShoppingBasket author:
                    _shoppingBaskets.Add(author);
                    break;
            }
        }

        public void Remove<EntityType>(EntityType entity)
        {
            switch (entity)
            {
                case Item book:
                    _items.Remove(book);
                    break;
                case ShoppingBasket author:
                    _shoppingBaskets.Remove(author);
                    break;
            }
        }

        public void SaveChanges()
        {
        }
    }
}
