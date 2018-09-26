using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Core.Domain
{
    public class MarketCollection
    {
        private readonly IPriceTicker _priceTicker;
        private readonly Dictionary<string, IMarket> _markets = new Dictionary<string, IMarket>();

        public IReadOnlyList<IMarket> Markets => _markets.Values.ToList();


        public MarketCollection(IPriceTicker priceTicker)
        {
            _priceTicker = priceTicker;
        }

        public void AddMarket(IMarket market)
        {
            _markets.Add(market.ProductCode, market);
        }


        public Task<ITick> GetCurrentPriceAsync(string productCode)
            => _priceTicker.GetCurrentPriceAsync(_markets[productCode]);

        private bool IsSubscribedToTicks(string productCode)
            => _priceTicker.IsSubscribedToTicks(_markets[productCode]);

        private IObservable<ITick> SubscribeToTicks(string productCode)
            => _priceTicker.SubscribeToTicks(_markets[productCode]);

        private void UnsubscribeFromTicks(string productCode)
            => _priceTicker.UnsubscribeFromTicks(_markets[productCode]);

    }
}