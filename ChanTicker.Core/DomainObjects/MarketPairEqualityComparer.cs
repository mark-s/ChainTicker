using System.Collections.Generic;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.DomainObjects
{
    internal class MarketPairEqualityComparer : IEqualityComparer<IMarket>
    {
        public bool Equals(IMarket cp1, IMarket cp2)
            => cp1?.Id == cp2?.Id;

        public int GetHashCode(IMarket cp)
            => cp.Id.GetHashCode();
    }
}