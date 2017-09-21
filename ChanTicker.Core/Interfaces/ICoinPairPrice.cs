namespace ChanTicker.Core.Interfaces
{
    public interface ICoinPairPrice
    {
        ICoinPair CoinPair { get; }

        ITick Tick { get; }

    }
}