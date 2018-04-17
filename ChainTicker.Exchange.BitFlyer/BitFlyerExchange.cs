using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;


namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange
    {
        private readonly IPriceService _priceService;

        public ExchangeInfo Info { get; }

        public List<Market> Markets { get; }

        internal BitFlyerExchange(ExchangeInfo exchangeInfo, List<Market> markets, IPriceService priceService)
        {
            _priceService = priceService;
            Info = exchangeInfo;
            Markets = markets;
        }


        public Task<ITick> GetCurrentPriceAsync(Market market)
            => _priceService.GetCurrentPriceAsync(market);

        public bool IsSubscribedToTicks(Market market)
            => _priceService.IsSubscribedToTicks(market);

        public IObservable<ITick> SubscribeToTicks(Market market)
            => _priceService.SubscribeToTicks(market);

        public void UnsubscribeFromTicks(Market market)
            => _priceService.UnsubscribeFromTicks(market);
        

    }
}
