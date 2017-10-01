using System;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Domain
{
    public class EmptyTick : ITick
    {
        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.Now;
        public decimal? Price { get; } = decimal.Zero;
    }
}