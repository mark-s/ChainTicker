using System;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Entities
{
    public class Tick : ITick
    {
        public DateTimeOffset TimeStamp { get; }
        public decimal? Price { get; }

        public Tick(decimal? price, DateTimeOffset timeStamp)
        {
            Price = price;
            TimeStamp = timeStamp;
        }
    }


}