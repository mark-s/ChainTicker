using System;

namespace ChainTicker.Core.Interfaces
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