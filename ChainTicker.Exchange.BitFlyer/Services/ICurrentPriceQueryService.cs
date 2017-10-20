using System;
using System.Threading.Tasks;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer.Services
{
    public interface ICurrentPriceQueryService
    {
        Task<ITick> GetCurrentPriceAsync(Market market);
        void StartListening();
        IObservable<ITick> Subscribe(Market market);
    }
}