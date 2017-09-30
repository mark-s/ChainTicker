using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.DomainObjects
{
    public class Market : IMarket
    {
        private static readonly MarketPairEqualityComparer _comparer = new MarketPairEqualityComparer();

        public string Base { get; }

        public string Counter { get; }

        public string Id => Base + Counter;

        public Market(string baseCoin, string counterCoin)
        {
            Base = baseCoin;
            Counter = counterCoin;
        }

        public bool Equals(IMarket x, IMarket y) => _comparer.Equals(x, y);

        public int GetHashCode(IMarket obj) => _comparer.GetHashCode(obj);
    }
}