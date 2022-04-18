using ShoppingBasketApi.Objects;

namespace ShoppingBasketApi.Services.Abstract
{
    public interface IShoppingBasketService
    {
        void AddItem(int id);
        void RemoveItem(int id);
        Task<IEnumerable<Item>> GetAvailableItems(string currency);
        Task<IEnumerable<Item>> GetBasket(string currency);
    }
}
