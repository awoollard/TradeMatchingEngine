public class Side {
    private SideEnum Name { get; }
    public List<Order> Orders;

    public Side(SideEnum side) {
        Name = side;
        Orders = new List<Order>();
    }
}