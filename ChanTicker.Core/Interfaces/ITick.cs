using System;

namespace ChanTicker.Core.Interfaces
{
    public interface ITick
    {
        DateTimeOffset TimeStamp { get; }
        decimal? Price { get; }
    }
}