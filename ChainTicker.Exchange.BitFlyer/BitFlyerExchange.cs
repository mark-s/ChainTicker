using System.Collections.Generic;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using EnsureThat;


namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange
    {
        public ExchangeInfo Info { get; }

        public List<IMarket> Markets { get; }

        internal BitFlyerExchange(ExchangeInfo exchangeInfo, List<IMarket> markets)
        {
            Info = EnsureArg.IsNotNull(exchangeInfo, nameof(exchangeInfo));
            Markets = EnsureArg.IsNotNull(markets, nameof(markets));
        }

    }
}
