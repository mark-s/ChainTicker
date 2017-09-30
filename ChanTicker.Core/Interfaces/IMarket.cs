using System.Collections.Generic;

namespace ChanTicker.Core.Interfaces
{
    public interface IMarketPair : IEqualityComparer<IMarketPair>
    {
        string Base { get; }

        string Counter { get; }

        string Id { get; }
    }
}