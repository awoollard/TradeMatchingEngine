public class Side {
    private SideEnum Name { get; }
    public List<Order> Orders;

    public Side(SideEnum side) {
        this.Name = side;
        this.Orders = new List<Order>();
    }
}