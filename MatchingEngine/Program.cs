 public class Program
{
    public static void Main(string[] args)
    {   
        var instruments = InputParser.ParseInput();

        // Match orders and print trades
        foreach (var trade in MatchingService.Process(instruments))
        {
            Console.WriteLine(trade.ToString());
        }
    }
}

