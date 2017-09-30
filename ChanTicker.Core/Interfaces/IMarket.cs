using System;
using System.Threading.Tasks;

namespace ChanTicker.Core.Interfaces
{
    public interface IMarket 
    {
        string Id { get; }

        string BaseCurrency { get; }

        string CounterCurrency { get; }

        Task<ITick> GetCurrentPriceAsync();

        IObservable<ITick> SubscribeToTicks();
    }

}