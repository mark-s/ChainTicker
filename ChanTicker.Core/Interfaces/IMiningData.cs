namespace ChanTicker.Core.Interfaces
{
    public interface IMiningData
    {
         string Algorithm { get; }
         bool IsFullyPremined { get; }
         string PreMinedValue { get; }
         string ProofType { get; }
         string TotalCoinsFreeFloat { get; }
         string TotalCoinSupply { get; }

    }
}