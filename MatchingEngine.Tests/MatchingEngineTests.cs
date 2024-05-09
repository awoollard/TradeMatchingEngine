using NUnit.Framework;

[TestFixture]
public class MatchingEngineTests
{
    [Test]
    public void MatchingService_Process_Example1_ReturnsExpectedTrades()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();
        orders["AUDUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["AUDUSD"].Item1.Orders.Add(new Order("A", "AUDUSD", 100, 1.47f, 1));
        orders["AUDUSD"].Item2.Orders.Add(new Order("B", "AUDUSD", -50, 1.45f, 2));

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades.Count, Is.EqualTo(1));
        Assert.That(trades[0].BuyerId, Is.EqualTo("A"));
        Assert.That(trades[0].SellerId, Is.EqualTo("B"));
        Assert.That(trades[0].Symbol, Is.EqualTo("AUDUSD"));
        Assert.That(trades[0].Quantity, Is.EqualTo(50));
        Assert.That(trades[0].Price, Is.EqualTo(1.47f));
    }

    [Test]
    public void MatchingService_Process_Example2_ReturnsExpectedTrades()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();
        orders["GBPUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["GBPUSD"].Item1.Orders.Add(new Order("A", "GBPUSD", 100, 1.66f, 1));
        orders["GBPUSD"].Item2.Orders.Add(new Order("C", "GBPUSD", -10, 1.5f, 2));
        orders["GBPUSD"].Item2.Orders.Add(new Order("C", "GBPUSD", -20, 1.6f, 3));
        orders["GBPUSD"].Item2.Orders.Add(new Order("C", "GBPUSD", -20, 1.7f, 4));

        orders["EURUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["EURUSD"].Item2.Orders.Add(new Order("B", "EURUSD", -100, 1.11f, 5));
        orders["EURUSD"].Item2.Orders.Add(new Order("F", "EURUSD", -50, 1.1f, 6));
        orders["EURUSD"].Item1.Orders.Add(new Order("D", "EURUSD", 100, 1.11f, 7));

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades.Count, Is.EqualTo(4));

        // Assert trade details
        Assert.That(trades[0].ToString(), Is.EqualTo("A:C:GBPUSD:10:1.66"));
        Assert.That(trades[1].ToString(), Is.EqualTo("A:C:GBPUSD:20:1.66"));
        Assert.That(trades[2].ToString(), Is.EqualTo("D:F:EURUSD:50:1.1"));
        Assert.That(trades[3].ToString(), Is.EqualTo("D:B:EURUSD:50:1.11"));
    }

    [Test]
    public void MatchingService_Process_EmptyOrders_ReturnsEmptyTradesList()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades, Is.Empty);
    }

    [Test]
    public void MatchingService_Process_SingleOrderWithNoMatch_ReturnsEmptyTradesList()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();
        orders["AUDUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["AUDUSD"].Item1.Orders.Add(new Order("A", "AUDUSD", 100, 1.47f, 1));

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades, Is.Empty);
    }

    [Test]
    public void MatchingService_Process_SingleValidMatch_ReturnsOneTrade()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();
        orders["AUDUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["AUDUSD"].Item1.Orders.Add(new Order("A", "AUDUSD", 100, 1.47f, 1));
        orders["AUDUSD"].Item2.Orders.Add(new Order("B", "AUDUSD", -50, 1.45f, 2));

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades.Count, Is.EqualTo(1));
        Assert.That(trades[0].BuyerId, Is.EqualTo("A"));
        Assert.That(trades[0].SellerId, Is.EqualTo("B"));
        Assert.That(trades[0].Symbol, Is.EqualTo("AUDUSD"));
        Assert.That(trades[0].Quantity, Is.EqualTo(50));
        Assert.That(trades[0].Price, Is.EqualTo(1.47f));
    }

    [Test]
    public void MatchingService_Process_MultipleValidMatches_ReturnsMultipleTrades()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();
        orders["AUDUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["AUDUSD"].Item1.Orders.Add(new Order("A", "AUDUSD", 100, 1.47f, 1));
        orders["AUDUSD"].Item2.Orders.Add(new Order("B", "AUDUSD", -50, 1.45f, 2));
        orders["EURUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["EURUSD"].Item1.Orders.Add(new Order("C", "EURUSD", 100, 1.2f, 3));
        orders["EURUSD"].Item2.Orders.Add(new Order("D", "EURUSD", -100, 1.19f, 4));

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades.Count, Is.EqualTo(2));
        // Assert trade details...
    }

    [Test]
    public void MatchingService_Process_NoMatch_ReturnsEmptyTradesList()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();
        orders["AUDUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["AUDUSD"].Item1.Orders.Add(new Order("A", "AUDUSD", 100, 1.44f, 1));
        orders["AUDUSD"].Item2.Orders.Add(new Order("B", "AUDUSD", -50, 1.47f, 2));

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades, Is.Empty);
    }

    [Test]
    public void MatchingService_Process_NoMatchingOrders_ReturnsEmptyTradesList()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();
        orders["AUDUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["AUDUSD"].Item1.Orders.Add(new Order("A", "AUDUSD", 100, 1.45f, 1));
        orders["AUDUSD"].Item2.Orders.Add(new Order("B", "AUDUSD", -50, 1.47f, 1));

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades, Is.Empty);
    }

    [Test]
    public void MatchingService_Process_NoMatchingInstruments_ReturnsEmptyTradesList()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();
        orders["AUDUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["AUDUSD"].Item1.Orders.Add(new Order("A", "AUDUSD", 100, 1.47f, 1));
        orders["EURUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["EURUSD"].Item2.Orders.Add(new Order("B", "EURUSD", -50, 1.45f, 1));

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades, Is.Empty);
    }

    [Test]
    public void MatchingService_Process_MatchingOrders_ReturnsCorrectQuantity()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();
        orders["AUDUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["AUDUSD"].Item1.Orders.Add(new Order("A", "AUDUSD", 100, 1.47f, 1));
        orders["AUDUSD"].Item2.Orders.Add(new Order("B", "AUDUSD", -50, 1.47f, 2));
        orders["AUDUSD"].Item2.Orders.Add(new Order("C", "AUDUSD", -25, 1.47f, 3));

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades.Count, Is.EqualTo(2));
        Assert.That(trades[0].Quantity, Is.EqualTo(50));
        Assert.That(trades[1].Quantity, Is.EqualTo(25));
    }
    
    [Test]
    public void MatchingService_Process_SamePriceDifferentSequenceId_TradesBasedOnSequenceId()
    {
        // Arrange
        var orders = new Dictionary<string, Tuple<Side, Side>>();
        orders["AUDUSD"] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
        orders["AUDUSD"].Item2.Orders.Add(new Order("A", "AUDUSD", -50, 1.47f, 1));
        orders["AUDUSD"].Item1.Orders.Add(new Order("B", "AUDUSD", 50, 1.47f, 7));
        orders["AUDUSD"].Item1.Orders.Add(new Order("C", "AUDUSD", 50, 1.47f, 5));

        // Act
        var trades = MatchingService.Process(orders);

        // Assert
        Assert.That(trades, Is.Not.Null);
        Assert.That(trades.Count, Is.EqualTo(1));
        Assert.That(trades[0].ToString(), Is.EqualTo("C:A:AUDUSD:50:1.47"));
    }
}
