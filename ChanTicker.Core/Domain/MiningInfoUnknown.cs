using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Domain
{
    public sealed class MiningInfoUnknown : IMiningData
    {
        public string Algorithm { get; } = string.Empty;
        public bool IsFullyPremined { get; } = false;
        public string PreMinedValue { get; } = string.Empty;
        public string ProofType { get; } = string.Empty;
        public string TotalCoinsFreeFloat { get; } = string.Empty;
        public string TotalCoinSupply { get; } = string.Empty;
    }
}