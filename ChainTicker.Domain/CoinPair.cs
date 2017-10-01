using System;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Domain
{
    public class CoinPair : ICoinPair
    {
        private static readonly CoinPairEqualityComparer _comparer = new CoinPairEqualityComparer();

        public ICoin Base { get; }
        public ICoin Counter { get; }

        public string Id => Base.Code + Counter.Code;

        public CoinPair(ICoin baseCoin, ICoin counterCoin)
        {
            Base = baseCoin ?? throw new ArgumentNullException(nameof(baseCoin), "base is required!");
            Counter = counterCoin ?? throw new ArgumentNullException(nameof(counterCoin), "counter is required!");
        }

        
        public bool Equals(ICoinPair x, ICoinPair y) => _comparer.Equals(x, y);

        public int GetHashCode(ICoinPair obj) => _comparer.GetHashCode(obj);
    }
}

