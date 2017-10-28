using System;
using System.Threading.Tasks;
using ChanTicker.Core.Domain;

namespace ChanTicker.Core.Interfaces
{
    public interface INotRealTimePriceService
    {
        Task<ITick> GetCurrentPriceAsync(Market market);

        IObservable<ITick> Subscribe(Market market);

        void Unubscribe(Market market);
    }
}