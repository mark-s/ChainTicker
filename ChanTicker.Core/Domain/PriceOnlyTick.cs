using System;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Domain
{
    public class PriceOnlyTick : ITick
    {
        public DateTimeOffset TimeStamp { get; }
        public decimal? LastTradedPrice { get; }
        public decimal BestAsk => 0;
        public decimal BestBid => 0;
        public double Volume  => 0;

        public PriceOnlyTick(decimal? price, DateTimeOffset timeStamp)
        {
            LastTradedPrice = price;
            TimeStamp = timeStamp;
        }
    }
}