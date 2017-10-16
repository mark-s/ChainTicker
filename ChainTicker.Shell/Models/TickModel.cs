using System;
using ChainTicker.Shell.Helpers;
using ChanTicker.Core.Interfaces;
using Prism.Mvvm;

namespace ChainTicker.Shell.Models
{
    public class TickModel : BindableBase
    {

        private PriceDirection _priceDirection;
        public PriceDirection PriceDirection
        {
            get => _priceDirection;
            private set => SetProperty(ref _priceDirection, value);
        }

        private decimal? _price;
        public decimal? Price
        {
            get => _price;
            private set => SetProperty(ref _price, value);
        }

        private DateTime _timeStamp;

        public TickModel(decimal initialPrice)
        {
            Price = initialPrice;
        }

        public DateTime TimeStamp
        {
            get => _timeStamp;
            private set => SetProperty(ref _timeStamp, value);
        }


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