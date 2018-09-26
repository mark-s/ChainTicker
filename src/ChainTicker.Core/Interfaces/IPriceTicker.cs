using System;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;

namespace ChainTicker.Core.Interfaces
{
    public interface IPriceTicker
    {
        Task<ITick> GetCurrentPriceAsync(IMarket market);

        IObservable<ITick> SubscribeToTicks(IMarket market);

        void UnsubscribeFromTicks(IMarket market);

        bool IsSubscribedToTicks(IMarket market);

    }
}