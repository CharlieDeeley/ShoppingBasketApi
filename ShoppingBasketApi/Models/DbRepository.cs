using ShoppingBasketApi.Objects;

namespace ShoppingBasketApi.Models
{
    public class DbRepository : IShoppingBasketRepository
    {
        private readonly ShoppingBasketContext _db;

        public DbRepository(ShoppingBasketContext db)
        {
            _db = db;
        }

        public IQueryable<Item> Items => _db.Items;
        public IQueryable<ShoppingBasket> ShoppingBasket => _db.ShoppingBasket;
        public void Add<EntityType>(EntityType entity) => _db.Add(entity);
        public void Remove<EntityType>(EntityType entityType) => _db.Remove(entityType);    
        public void SaveChanges() => _db.SaveChanges();
    }
}
