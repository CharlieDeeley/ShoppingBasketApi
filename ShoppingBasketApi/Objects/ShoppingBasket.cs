namespace ShoppingBasketApi.Objects
{
    public class ShoppingBasket
    {
        public int Id { get; set; }
        public ICollection<Item> Basket { get; set; }
    }
}
