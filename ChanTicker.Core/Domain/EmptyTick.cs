using System;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Domain
{
    public class EmptyTick : ITick
    {
        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.Now;
        public decimal? LastTradedPrice { get; } = decimal.Zero;
        public decimal BestAsk { get; } = decimal.Zero;
        public decimal BestBid { get; } = decimal.Zero;
        public double Volume { get; } = double.NaN;
    }
}