public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter orders. You can press enter without any input to end: ");
        
        var instruments = InputParser.ParseInput();

        // Match orders and print trades
        foreach (var trade in MatchingService.Process(instruments))
        {
            Console.WriteLine(trade.ToString());
        }
    }
}

