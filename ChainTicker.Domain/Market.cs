using System;
using System.Threading.Tasks;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Domain
{
    public class Market : IMarket
    {
        private readonly IMarketDataSource _dataSource;
        public string Id { get; }
        public string BaseCurrency { get; }
        public string CounterCurrency { get; }


        public Market(IMarketDataSource dataSource, string id, string baseCurrency, string counterCurrency)
        {
            _dataSource = dataSource;
            Id = id;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
        }

        public async Task<ITick> GetCurrentPriceAsync()
        {
            return await _dataSource.GetCurrentPriceForMarketAsync(Id);
        }

        public IObservable<ITick> SubscribeToTicks()
        {
            throw new NotImplementedException();
        }
    }
}