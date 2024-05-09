public static class InputParser {
    public static Dictionary<string, Tuple<Side, Side>> ParseInput() { 
        var orders = new Dictionary<string, Tuple<Side, Side>>();

        string line;
        int sequenceId = 0;
        do
        {
            string participantId, symbol;
            int quantity;
            float price;
            if(!ReadInput(out line, out participantId, out symbol, out quantity, out price)) {
                continue;
            }

            // Initialise a buy side and a sell side for a new symbol
            if (!orders.ContainsKey(symbol))
            {
                orders[symbol] = new Tuple<Side, Side>(new Side(SideEnum.BUY), new Side(SideEnum.SELL));
            }

            // Positive quantities go to buy side
            if (quantity > 0)
            {
                orders[symbol].Item1.Orders.Add(new Order(participantId, symbol, quantity, price, sequenceId));
            }
            
            // Negative quantities go to buy side
            if (quantity < 0)
            {
                orders[symbol].Item2.Orders.Add(new Order(participantId, symbol, quantity, price, sequenceId));
            }

            // quantity == 0 ignored
            sequenceId++;
        } while (!string.IsNullOrEmpty(line));

        return orders;
    }

    private static bool ReadInput(out string line, out string participantId, out string symbol, out int quantity, out float price)
    {
        #pragma warning disable CS8601
        line = Console.ReadLine();
        #pragma warning restore CS8601

        if (string.IsNullOrEmpty(line))
        {
            return ReadError(out line, out participantId, out symbol, out quantity, out price);
        }

        string[] parts = line.Split(':');
        if (parts.Length != 4)
        {
            Console.WriteLine("Invalid input format. Please enter again.");
            return ReadError(out line, out participantId, out symbol, out quantity, out price);
        }

        participantId = parts[0];
        symbol = parts[1];
        if (!int.TryParse(parts[2], out quantity))
        {
            Console.WriteLine("Invalid amount. Please enter a valid integer number.");
            return ReadError(out line, out participantId, out symbol, out quantity, out price);
        }
        if (!float.TryParse(parts[3], out price))
        {
            Console.WriteLine("Invalid price. Please enter a valid float number.");
            return ReadError(out line, out participantId, out symbol, out quantity, out price);
        }
        
        return true;
    }

    private static bool ReadError(out string line, out string participantId, out string symbol, out int quantity, out float price)
    {
        line = string.Empty;
        participantId = string.Empty;
        symbol = string.Empty;
        quantity = 0;
        price = 0;
        return false;
    }
}