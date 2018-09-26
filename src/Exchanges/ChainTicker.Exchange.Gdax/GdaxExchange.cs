using System.Collections.Generic;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Exchange.Gdax
{
    public class GdaxExchange : IExchange
    {
        public ExchangeInfo Info { get; }
        public IReadOnlyList<IMarket> Markets { get; }
        
        internal GdaxExchange(ExchangeInfo exchangeInfo, List<IMarket> markets)
        {
            Info = exchangeInfo;
            Markets = markets;
       }



    }
}
