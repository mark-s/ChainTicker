using System;
using System.Threading.Tasks;
using ChanTicker.Core.Domain;

namespace ChanTicker.Core.Interfaces
{
    public interface IPriceDataService
    {
        Task<ITick> GetCurrentPriceAsync(Market market);

        IObservable<ITick> SubscribeToTicks(Market market);

        void UnsubscribeFromTicks(Market market);

        bool IsSubscribedToTicks(Market market);

    }
}