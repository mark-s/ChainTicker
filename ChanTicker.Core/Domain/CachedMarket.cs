using System;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Core.Domain
{
    public class CachedMarket : IMarket
    {
        public string ProductCode { get; set; }

        public string BaseCurrency { get; set; }

        public string CounterCurrency { get; set; }

        public string DisplayName { get; set; }

        public bool HasRealTimeUpdates { get; set; }

        public Task<ITick> GetCurrentPriceAsync() => throw new NotSupportedException();

        public bool IsSubscribedToTicks() => throw new NotSupportedException();

        public IObservable<ITick> SubscribeToTicks() => throw new NotSupportedException();

        public void UnsubscribeFromTicks() => throw new NotSupportedException();

    }
}
