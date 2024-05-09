using NUnit.Framework;

[TestFixture]
public class TradeTests
{
    [Test]
    public void Trade_ToString_ReturnsExpectedString()
    {
        // Arrange
        var trade = new Trade("A", "B", "EURUSD", 100, 1.2f);

        // Act
        var result = trade.ToString();

        // Assert
        Assert.AreEqual("A:B:EURUSD:100:1.2", result);
    }
}