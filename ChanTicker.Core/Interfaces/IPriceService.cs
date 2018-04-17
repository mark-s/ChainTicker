using System;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;

namespace ChainTicker.Core.Interfaces
{
    public interface IPriceService : IDisposable
    {
        Task<ITick> GetCurrentPriceAsync(Market market);

        IObservable<ITick> SubscribeToTicks(Market market);

        void UnsubscribeFromTicks(Market market);

        bool IsSubscribedToTicks(Market market);

    }
}