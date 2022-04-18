using ShoppingBasketApi.Objects;

namespace ShoppingBasketApi.Models
{
    public interface IShoppingBasketRepository
    {
        public IQueryable<Item> Items { get; }
        public IQueryable<ShoppingBasket> ShoppingBasket { get; }

        void Add<EntityType>(EntityType entity);
        void Remove<EntityType>(EntityType entityType);
        void SaveChanges();
    }
}
