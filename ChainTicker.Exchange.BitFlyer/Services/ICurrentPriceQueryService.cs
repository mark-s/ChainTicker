using System;
using System.Threading.Tasks;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer.Services
{
    public interface ICurrentPriceQueryService
    {
        Task<ITick> GetCurrentPriceAsync(Market market);

        IObservable<ITick> Subscribe(Market market);
        void Unubscribe(Market market);
    }
}