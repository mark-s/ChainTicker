using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Domain
{
    public sealed class MiningInfo : IMiningData
    {
        public string Algorithm { get; }
        public bool IsFullyPremined { get; }
        public string PreMinedValue { get; }
        public string ProofType { get; }
        public string TotalCoinsFreeFloat { get; }
        public string TotalCoinSupply { get; }



        public MiningInfo(bool isFullyPremined,
                                    string preMinedValue,
                                    string proofType,
                                    string totalCoinsFreeFloat,
                                    string totalCoinSupply,
                                    string algorithm)
        {
            IsFullyPremined = isFullyPremined;
            PreMinedValue = preMinedValue;
            ProofType = proofType;
            TotalCoinsFreeFloat = totalCoinsFreeFloat;
            TotalCoinSupply = totalCoinSupply;
            Algorithm = algorithm;
        }

    }
}