using Microsoft.EntityFrameworkCore;
using ShoppingBasketApi.Objects;

namespace ShoppingBasketApi.Models
{
    public class ShoppingBasketContext : DbContext
    {

        public ShoppingBasketContext(DbContextOptions<ShoppingBasketContext> options) : base(options)
        { 

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ShoppingBasket> ShoppingBasket { get; set; }
    }
}
