using System.Collections.Generic;
using ChainTicker.Core.Domain;

namespace ChainTicker.Core.Interfaces
{
    public interface IExchange 
    {

        ExchangeInfo Info { get; }

        List<IMarket> Markets { get; }


    }
}
