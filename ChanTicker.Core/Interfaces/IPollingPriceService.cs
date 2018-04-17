using System;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;

namespace ChainTicker.Core.Interfaces
{
    public interface IPollingPriceService
    {
        Task<ITick> GetCurrentPriceAsync(Market market);

        IObservable<ITick> Subscribe(Market market);

        void Unubscribe(Market market);
    }
}