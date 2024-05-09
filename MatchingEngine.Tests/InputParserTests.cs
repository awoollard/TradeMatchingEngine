using NUnit.Framework;

[TestFixture]
public class InputParserTests
{
    [Test]
    public void InputParser_ParseInput_EmptyInput_ReturnsEmptyDictionary()
    {
        // Arrange
        string input = string.Empty;
        StringReader reader = new StringReader(input);
        Console.SetIn(reader);

        // Act
        var result = InputParser.ParseInput();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void InputParser_ParseInput_SingleValidInput_ReturnsDictionaryWithOneItem()
    {
        // Arrange
        string input = "A:AUDUSD:100:1.47\n";

        // Act
        StringReader reader = new StringReader(input);
        Console.SetIn(reader);
        var result = InputParser.ParseInput();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.ContainsKey("AUDUSD"), Is.True);
    }

    [Test]
    public void InputParser_ParseInput_MultipleValidInputs_ReturnsDictionaryWithMultipleItems()
    {
        // Arrange
        string input = "A:AUDUSD:100:1.47\nB:AUDUSD:-50:1.45\nC:EURUSD:200:1.2\n";

        // Act
        StringReader reader = new StringReader(input);
        Console.SetIn(reader);
        var result = InputParser.ParseInput();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.ContainsKey("AUDUSD"), Is.True);
        Assert.That(result.ContainsKey("EURUSD"), Is.True);
    }
    
    [Test]
    public void InputParser_ParseInput_InvalidInputFormat_ReturnsEmptyDictionary()
    {
        // Arrange
        string input = "InvalidInputFormat\n";

        // Act
        StringReader reader = new StringReader(input);
        Console.SetIn(reader);
        var result = InputParser.ParseInput();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void InputParser_ParseInput_InvalidQuantity_ReturnsEmptyDictionary()
    {
        // Arrange
        string input = "A:AUDUSD:invalid_quantity:1.47\n";

        // Act
        StringReader reader = new StringReader(input);
        Console.SetIn(reader);
        var result = InputParser.ParseInput();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }
}