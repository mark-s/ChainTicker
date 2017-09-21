using System;

namespace ChanTicker.Core.Interfaces
{
    public interface ITick
    {
        DateTimeOffset TimeStamp { get; }
        double? Price { get; }
    }
}