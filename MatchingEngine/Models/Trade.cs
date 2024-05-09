

public class Trade {
    public string BuyerId { get; set; }
    public string SellerId { get; set; }
    public string Symbol { get; set;}
    public int Quantity { get; set;}
    public float Price { get; set; }
    
    public Trade(string buyerId, string sellerId, string symbol, int quantity, float price) {
        BuyerId = buyerId;
        SellerId = sellerId;
        Symbol = symbol;
        Quantity = quantity;
        Price = price;
    }

    public override string ToString() {
        return $"{BuyerId}:{SellerId}:{Symbol}:{Quantity}:{Price}";
    }
}