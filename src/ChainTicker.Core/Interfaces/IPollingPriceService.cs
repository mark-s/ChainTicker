using System;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;

namespace ChainTicker.Core.Interfaces
{
    public interface IPollingPriceService
    {
        Task<ITick> GetCurrentPriceAsync(IMarket market);

        IObservable<ITick> Subscribe(IMarket market);

        void Unubscribe(IMarket market);
    }
}