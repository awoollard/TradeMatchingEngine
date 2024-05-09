

public class Trade {
    public string buyerId { get; set; }
    public string sellerId { get; set; }
    public string symbol { get; set;}
    public int quantity { get; set;}
    public float price { get; set; }
    
    public Trade(string buyerId, string sellerId, string symbol, int quantity, float price) {
        this.buyerId = buyerId;
        this.sellerId = sellerId;
        this.symbol = symbol;
        this.quantity = quantity;
        this.price = price;
    }

    public override string ToString() {
        return $"{buyerId}:{sellerId}:{symbol}:{quantity}:{price}";
    }
}