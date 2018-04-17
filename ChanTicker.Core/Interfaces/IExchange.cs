using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;

namespace ChainTicker.Core.Interfaces
{
    public interface IExchange 
    {

        ExchangeInfo Info { get; }

        List<Market> Markets { get; }


        Task<ITick> GetCurrentPriceAsync(Market market);
            
        bool IsSubscribedToTicks(Market market);
            
        IObservable<ITick> SubscribeToTicks(Market market);

         void UnsubscribeFromTicks(Market market);

    }
}
