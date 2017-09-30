using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.DomainObjects
{
    public class CoinPairPrice : ICoinPairPrice
    {
        public ICoinPair CoinPair { get; }

        public ITick Tick { get; }

        public CoinPairPrice(ICoinPair coinPair, ITick tick)
        {
            CoinPair = coinPair;
            Tick = tick;
        }


    }
}
