using NUnit.Framework;

[TestFixture]
public class SideTests
{
    [Test]
    public void Side_AddOrder_OrderAddedSuccessfully()
    {
        // Arrange
        var side = new Side(SideEnum.BUY);
        var order = new Order("A", "EURUSD", 100, 1.2f, 1);

        // Act
        side.Orders.Add(order);

        // Assert
        Assert.That(side.Orders.Count, Is.EqualTo(1));
        Assert.That(order, Is.SameAs(side.Orders[0]));
    }

    [Test]
    public void Side_RemoveOrder_OrderRemovedSuccessfully()
    {
        // Arrange
        var side = new Side(SideEnum.BUY);
        var order = new Order("A", "EURUSD", 100, 1.2f, 1);
        side.Orders.Add(order);

        // Act
        side.Orders.Remove(order);

        // Assert
        Assert.That(side.Orders.Count, Is.EqualTo(0));
    }
}