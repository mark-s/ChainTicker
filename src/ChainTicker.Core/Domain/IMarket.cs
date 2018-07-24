using System;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Core.Domain
{
    public interface IMarket
    {
        string BaseCurrency { get; }

        string CounterCurrency { get; }

        string DisplayName { get; }

        bool HasRealTimeUpdates { get; }

        string ProductCode { get; }

        Task<ITick> GetCurrentPriceAsync();

        bool IsSubscribedToTicks();

        IObservable<ITick> SubscribeToTicks();

        void UnsubscribeFromTicks();
    }
}