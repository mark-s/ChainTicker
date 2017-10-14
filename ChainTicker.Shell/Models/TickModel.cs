using System;
using System.ComponentModel;
using ChainTicker.Shell.Helpers;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Shell.Models
{
    public class TickModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PriceDirection PriceDirection { get; private set; } = PriceDirection.Level;

        public decimal? Price { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public void Update(ITick tick)
        {
            SetPrice(tick.Price);
            TimeStamp = tick.TimeStamp.ToLocalTime().DateTime;
        }

        private void SetPrice(decimal? newPrice)
        {
            PriceDirection = PriceDirectionCalculator.GetPriceDirection(Price, newPrice);
            Price = newPrice;
        }

    }
}