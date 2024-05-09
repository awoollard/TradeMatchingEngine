public class Order {
    public string ParticipantId { get; set; }
    public string Symbol { get; set;}
    public int Quantity { get; set;}
    public float Price { get; set; }
    public int SequenceId { get; set; }
    
    public Order(string participantId, string symbol, int quantity, float price, int sequenceId) {
        ParticipantId = participantId;
        Symbol = symbol;
        Quantity = quantity;
        Price = price;
        SequenceId = sequenceId;
    }
}