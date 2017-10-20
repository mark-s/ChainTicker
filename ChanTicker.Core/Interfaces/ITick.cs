using System;

namespace ChanTicker.Core.Interfaces
{
    public interface ITick
    {
        DateTimeOffset TimeStamp { get; }

        decimal? LastTradedPrice { get; }

        decimal  BestAsk { get; }

        decimal  BestBid { get;  }
        double Volume { get;  }
    }
}