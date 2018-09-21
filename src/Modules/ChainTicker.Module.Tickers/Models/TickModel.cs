using System;
using ChainTicker.Module.Tickers.Helpers;
using Prism.Mvvm;

namespace ChainTicker.Module.Tickers.Models
{
    public class TickModel : BindableBase
    {

        private PriceDirection _priceDirection;
        public PriceDirection PriceDirection
        {
            get => _priceDirection;
            private set =>  SetProperty(ref _priceDirection, value);
        }


        private decimal? _price;
        public decimal? Price
        {
            get => _price;
            private set => SetProperty(ref _price, value);
        }


        private DateTime _timeStamp;
        public DateTime TimeStamp
        {
            get => _timeStamp;
            private set => SetProperty(ref _timeStamp, value);
        }


        private decimal _bestBid;
        public decimal BestBid
        {
            get => _bestBid;
            private set => SetProperty(ref _bestBid, value);
        }


        private decimal? _bestAsk;
        public decimal? BestAsk
        {
            get => _bestAsk;
            private set => SetProperty(ref _bestAsk, value);
        }


        private double _volume;
        public double Volume
        {
            get => _volume;
            private set => SetProperty(ref _volume, value);
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