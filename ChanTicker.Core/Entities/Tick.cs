using System;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Entities
{
    public class Tick : ITick
    {
        public DateTimeOffset TimeStamp { get; }
        public double? Price { get; }

        public Tick(double? price, DateTimeOffset timeStamp)
        {
            Price = price;
            TimeStamp = timeStamp;
        }

    }
}