using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChanTicker.Core.Domain;

namespace ChanTicker.Core.Interfaces
{
    public interface IExchange
    {

        ExchangeInfo Info { get; }

        Task<List<Market>> GetAvailableMarketsAsync();


         Task<ITick> GetCurrentPriceAsync(Market market);
            

         bool IsSubscribedToTicks(Market market);
            

         IObservable<ITick> SubscribeToTicks(Market market);

         void UnsubscribeFromTicks(Market market);

    }
}
