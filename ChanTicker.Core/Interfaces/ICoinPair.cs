using System.Collections.Generic;

namespace ChanTicker.Core.Interfaces
{

    /// <summary>
    /// A Pair of 'Coins' eg: a tradeable pair
    /// </summary>
    public interface ICoinPair : IEqualityComparer<ICoinPair>
    {
        ICoin Base { get; }

        ICoin Counter { get; }

        string Id { get; }
    }
}