using System;
using ChainTicker.App.Helpers;
using ChainTicker.Core.Interfaces;
using GalaSoft.MvvmLight;

namespace ChainTicker.App.Models
{
    public class TickModel : ViewModelBase
    {

        private PriceDirection _priceDirection;
        public PriceDirection PriceDirection
        {
            get => _priceDirection;
            private set => Set(ref _priceDirection, value);
        }


        private decimal? _price;
        public decimal? Price
        {
            get => _price;
            private set => Set(ref _price, value);
        }


        private DateTime _timeStamp;
        public DateTime TimeStamp
        {
            get => _timeStamp;
            private set => Set(ref _timeStamp, value);
        }


        private decimal _bestBid;
        public decimal BestBid
        {
            get => _bestBid;
            private set => Set(ref _bestBid, value);
        }


        private decimal? _bestAsk;
        public decimal? BestAsk
        {
            get => _bestAsk;
            private set => Set(ref _bestAsk, value);
        }


        private double _volume;
        public double Volume
        {
            get => _volume;
            private set => Set(ref _volume, value);
        }


        internal TickModel(decimal initialPrice)
        {
            Price = initialPrice;
        }

        internal void Update(ITick tick)
        {
            PriceDirection = PriceDirectionCalculator.GetPriceDirection(Price, tick.LastTradedPrice, PriceDirection);
            Price = tick.LastTradedPrice;
            TimeStamp = tick.TimeStamp.ToLocalTime().DateTime;
            BestAsk = tick.BestAsk;
            BestBid = tick.BestBid;
            Volume = tick.Volume;
        }

    }
}