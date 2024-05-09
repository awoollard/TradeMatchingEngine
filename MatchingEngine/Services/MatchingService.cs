public static class MatchingService
{
    /// <summary>
    /// Takes a diction
    /// </summary>
    /// <param name="instruments">The dictionary of instruments containing both buy and sell sides of the orders</param>
    /// <returns>The aggregated list of executed trades for each instrument</returns>
    public static List<Trade> Process(Dictionary<string, Tuple<Side, Side>> instruments)
    {
        List<Trade> trades = [];
        
        // Sort orders by Price and sequence
        SortSides(instruments);

        foreach (var instrument in instruments)
        {
            trades.AddRange(ProcessInstrument(instrument.Value.Item1, instrument.Value.Item2));
        }
        return trades;
    }

    /// <summary>
    /// Sorts both sides of a given instrument.
    /// The buy side is sorted by prices in descending order, then ascending sequence ID.
    /// The sell side is sorted by prices in ascending order, then descending sequence ID.
    /// 
    /// </summary>
    /// <param name="instruments">The dictionary of instruments containing both buy and sell sides of the orders</param>
    public static void SortSides(Dictionary<string, Tuple<Side, Side>> instruments)
    {
        foreach (var instrument in instruments)
        {
            var buySide = instrument.Value.Item1;
            var sellSide = instrument.Value.Item2;

            instrument.Value.Item1.Orders = instrument.Value.Item1.Orders
                .OrderByDescending(x => x.Price)
                .ThenBy(x => x.SequenceId).ToList();
            
            instrument.Value.Item2.Orders = instrument.Value.Item2.Orders
                .OrderBy(x => x.Price)
                .ThenBy(x => x.SequenceId).ToList();
        }
    }

    /// <summary>
    /// Processes both sides containing orders for a given instrument.
    /// </summary>
    /// <param name="buySide">Buy side</param>
    /// <param name="sellSide">Sell side</param>
    /// <returns>List of executed trades</returns>
    public static List<Trade> ProcessInstrument(Side buySide, Side sellSide)
    {
        List<Trade> trades = new List<Trade>();
        foreach (var buy in buySide.Orders.ToList())
        {
            var eligibleSells = sellSide.Orders
                .Where(x => x.Price <= buy.Price)
                .ToList();
            
            foreach(var sell in eligibleSells)
            {
                // Buy quantity could be zero if order is fully filled
                if (buy.Quantity == 0)
                    break;

                // Trade size is the minimum of buy order quantity and lowest Price sell quantity
                var tradeSize = Math.Min(Math.Abs(buy.Quantity), Math.Abs(sell.Quantity));

                float tradePrice;
                if(buy.SequenceId < sell.SequenceId) {
                    // Buy order was first
                    tradePrice = buy.Price;
                } else {
                    // Sell order was first
                    tradePrice = sell.Price;
                }

                // Match! Record the trade and update both sides of the instrument.
                trades.Add(new Trade(buy.ParticipantId, sell.ParticipantId, sell.Symbol, tradeSize, tradePrice));
                UpdateSides(buySide, sellSide, buy, sell, tradeSize);
            }
        }

        return trades;
    }

    /// <summary>
    /// Adjust order quantities for each side of an instrument.
    /// </summary>
    /// <param name="buySide">Buy side</param>
    /// <param name="sellSide">Sell side</param>
    /// <param name="buy">Buy order</param>
    /// <param name="sell">Sell order</param>
    /// <param name="tradeSize">The size of the trade, specified in units of the instrument</param>
    private static void UpdateSides(Side buySide, Side sellSide, Order buy, Order sell, int tradeSize)
    {
        // Sell side quantity is already negative so needs to be flipped to positive, then we subtract trade size.
        var sellQuantity = Math.Abs(sell.Quantity) - tradeSize;
        if (sellQuantity == 0)
        {
            // Sell side fully filled, remove it from sell side orders.
            sellSide.Orders.Remove(sell);
        } else {
            // Update sell side quantity, flipping quantity back to negative
            sell.Quantity = -sellQuantity;
        }


        // Update buy side quantity
        var buyQuantity = buy.Quantity - tradeSize;
        if (buyQuantity == 0)
        {
            // Buy side fully filled, remove it from buy side orders.
            buySide.Orders.Remove(buy);
        } else {
            // Update buy side quantity
            buy.Quantity = buyQuantity;
        }
    }
}