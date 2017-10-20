using System;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Domain
{
    public class Tick : ITick
    {
        public DateTimeOffset TimeStamp { get; }
        public decimal? LastTradedPrice { get; }
        public decimal BestAsk { get;  }
        public decimal BestBid { get; }
        public double Volume { get;  }

        public Tick(decimal? price, DateTimeOffset timeStamp, decimal bestAsk, decimal bestBid, double volume)
        {
            LastTradedPrice = price;
            TimeStamp = timeStamp;
            BestAsk = bestAsk;
            BestBid = bestBid;
            Volume = volume;
        }
    }
}