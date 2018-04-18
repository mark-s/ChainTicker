using System.Collections.Generic;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;


namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange
    {
        public ExchangeInfo Info { get; }

        public List<IMarket> Markets { get; }

        internal BitFlyerExchange(ExchangeInfo exchangeInfo, List<IMarket> markets)
        {
            Info = exchangeInfo;
            Markets = markets;
        }

    }
}
