namespace Basket.API.Models;

public class BasketItem
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
}

public class ShoppingCart
{
    public string UserName { get; set; } = string.Empty;
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    public ShoppingCart()
    {
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public decimal TotalPrice
    {
        get
        {
            decimal totalprice = 0;
            foreach (var item in Items)
            {
                totalprice += item.Price * item.Quantity;
            }
            return totalprice;
        }
    }
}
