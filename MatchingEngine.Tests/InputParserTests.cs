using NUnit.Framework;

[TestFixture]
public class InputParserTests
{
    // [Test]
    // public void InputParser_ParseInput_EmptyInput_ReturnsEmptyDictionary()
    // {
    //     // Arrange & Act
    //     var result = InputParser.ParseInput();

    //     // Assert
    //     Assert.IsNotNull(result);
    //     Assert.IsEmpty(result);
    // }

    // [Test]
    // public void InputParser_ParseInput_SingleValidInput_ReturnsDictionaryWithOneItem()
    // {
    //     // Arrange
    //     string input = "A:AUDUSD:100:1.47\n";

    //     // Act
    //     System.IO.StringReader reader = new System.IO.StringReader(input);
    //     Console.SetIn(reader);
    //     var result = InputParser.ParseInput();

    //     // Assert
    //     Assert.IsNotNull(result);
    //     Assert.AreEqual(1, result.Count);
    //     Assert.IsTrue(result.ContainsKey("AUDUSD"));
    // }

    // [Test]
    // public void InputParser_ParseInput_MultipleValidInputs_ReturnsDictionaryWithMultipleItems()
    // {
    //     // Arrange
    //     string input = "A:AUDUSD:100:1.47\nB:AUDUSD:-50:1.45\nC:EURUSD:200:1.2\n";

    //     // Act
    //     System.IO.StringReader reader = new System.IO.StringReader(input);
    //     Console.SetIn(reader);
    //     var result = InputParser.ParseInput();

    //     // Assert
    //     Assert.IsNotNull(result);
    //     Assert.AreEqual(2, result.Count);
    //     Assert.IsTrue(result.ContainsKey("AUDUSD"));
    //     Assert.IsTrue(result.ContainsKey("EURUSD"));
    // }
    
    [Test]
    public void InputParser_ParseInput_InvalidInputFormat_ReturnsEmptyDictionary()
    {
        // Arrange
        string input = "InvalidInputFormat\n";

        // Act
        System.IO.StringReader reader = new System.IO.StringReader(input);
        Console.SetIn(reader);
        var result = InputParser.ParseInput();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    [Test]
    public void InputParser_ParseInput_InvalidQuantity_ReturnsEmptyDictionary()
    {
        // Arrange
        string input = "A:AUDUSD:invalid_quantity:1.47\n";

        // Act
        System.IO.StringReader reader = new System.IO.StringReader(input);
        Console.SetIn(reader);
        var result = InputParser.ParseInput();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }
}