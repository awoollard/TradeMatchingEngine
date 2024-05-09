

public class Order {
    public string participantId { get; set; }
    public string symbol { get; set;}
    public int quantity { get; set;}
    public float price { get; set; }
    public int sequenceId { get; set; }
    
    public Order(string participantId, string symbol, int quantity, float price, int sequenceId) {
        this.participantId = participantId;
        this.symbol = symbol;
        this.quantity = quantity;
        this.price = price;
        this.sequenceId = sequenceId;
    }
}