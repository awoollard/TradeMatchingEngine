public static class MatchingService
{
    public static List<Trade> Process(Dictionary<string, Tuple<Side, Side>> instruments)
    {
        List<Trade> trades = [];
        
        // Sort orders by price and sequence
        SortSides(instruments);

        foreach (var instrument in instruments)
        {
            trades.AddRange(ProcessInstrument(instrument.Value.Item1, instrument.Value.Item2));
        }
        return trades;
    }

    public static void SortSides(Dictionary<string, Tuple<Side, Side>> instruments)
    {
        foreach (var instrument in instruments)
        {
            var buySide = instrument.Value.Item1;
            var sellSide = instrument.Value.Item2;

            instrument.Value.Item1.Orders = instrument.Value.Item1.Orders
                .OrderByDescending(x => x.price)
                .ThenBy(x => x.sequenceId).ToList();
            
            instrument.Value.Item2.Orders = instrument.Value.Item2.Orders
                .OrderBy(x => x.price)
                .ThenBy(x => x.sequenceId).ToList();
        }
    }

    public static List<Trade> ProcessInstrument(Side buySide, Side sellSide)
    {
        List<Trade> trades = new List<Trade>();
        foreach (var buy in buySide.Orders.ToList())
        {
            var eligibleSells = sellSide.Orders
                .Where(x => x.price <= buy.price)
                .ToList();
            
            foreach(var sell in eligibleSells)
            {
                // Sanity check sell object
                // Buy quantity could be zero if order is fully filled
                if (buy.quantity == 0)
                    break;

                // Trade size is the minimum of buy order quantity and lowest price sell quantity
                var tradeSize = Math.Min(Math.Abs(buy.quantity), Math.Abs(sell.quantity));

                float tradePrice;
                if(buy.sequenceId < sell.sequenceId) {
                    // Buy order was first
                    tradePrice = buy.price;
                } else {
                    // Sell order was first
                    tradePrice = sell.price;
                }

                // Match! Record the trade and update both sides of the instrument.
                trades.Add(new Trade(buy.participantId, sell.participantId, sell.symbol, tradeSize, tradePrice));
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
        var sellQuantity = Math.Abs(sell.quantity) - tradeSize;
        if (sellQuantity == 0)
        {
            // Sell side fully filled, remove it from sell side orders.
            sellSide.Orders.Remove(sell);
        } else {
            // Update sell side quantity, flipping quantity back to negative
            sell.quantity = -sellQuantity;
        }


        // Update buy side quantity
        var buyQuantity = buy.quantity - tradeSize;
        if (buyQuantity == 0)
        {
            // Buy side fully filled, remove it from buy side orders.
            buySide.Orders.Remove(buy);
        } else {
            // Update buy side quantity
            buy.quantity = buyQuantity;
        }
    }
}