using System.Collections.Generic;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Domain
{
    public class CoinPairEqualityComparer : IEqualityComparer<ICoinPair>
    {
        public bool Equals(ICoinPair cp1, ICoinPair cp2)
            => cp1?.Id == cp2?.Id;

        public int GetHashCode(ICoinPair cp)
            => cp.Id.GetHashCode();
    }
}