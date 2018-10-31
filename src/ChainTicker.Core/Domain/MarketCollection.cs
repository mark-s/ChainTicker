using System.Collections.Generic;

namespace ChainTicker.Core.Domain
{
    public class MarketCollection
    {
        private readonly List<IMarket> _markets = new List<IMarket>();

        public IReadOnlyList<IMarket> Markets => _markets.AsReadOnly();

        public MarketCollection(IEnumerable<IMarket> markets)
        {
            foreach (var market in markets)
                _markets.Add(market);
        }
    }
}