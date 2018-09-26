using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;

namespace ChainTicker.Core.Interfaces
{
    public interface IExchange 
    {

        ExchangeInfo Info { get; }

        MarketCollection Markets { get; }

   

    }
}
