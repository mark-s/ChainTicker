using System.Collections.Generic;
using System.Linq;

namespace ChainTicker.Core.Domain
{
    public class MarketCollection
    {
        private readonly Dictionary<string, IMarket> _markets = new Dictionary<string, IMarket>();

        public IReadOnlyList<IMarket> Markets => _markets.Values.ToList();

        public MarketCollection(IEnumerable<IMarket> markets)
        {
            foreach (var market in markets)
                AddMarket(market);
        }

        private void AddMarket(IMarket market) 
            => _markets.Add(market.ProductCode, market);
    }
}