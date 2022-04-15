using ShoppingBasketApi.Objects;

namespace ShoppingBasketApi.Models.Abstract
{
    public interface IShoppingBasket
    {
        IEnumerable<BasketItem> GetAvailableItems(double rate);
        void AddToBasket(int basketId, int itemId);
        void RemoveFromBasket(int basketId, int itemId);
        IEnumerable<BasketItem> GetBasket(int basketId, double rate);
        int CreateBasket();
    }
}
