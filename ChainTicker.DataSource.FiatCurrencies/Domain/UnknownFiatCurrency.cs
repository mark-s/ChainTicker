using System.Diagnostics;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.FiatCurrencies.Domain
{
    [DebuggerDisplay("{" + nameof(Description) + "}")]
    public class UnknownFiatCurrency : ICoin
    {
        public bool IsValid  => false;

        public string Code { get; }

        public string Name { get; } 

        public string Description { get; } = "Unknown Coin";


        public string InfoUrl { get; } = string.Empty;

        public string ImageUrl { get; } = string.Empty;


        public string Algorithm { get; } = string.Empty;
        public string ImageUrlShort { get; } = string.Empty;
        public string InfoUrlShort { get; } = string.Empty;
        public string ImageUrlFull { get; } = string.Empty;
        public string InfoUrlFull { get; } = string.Empty;
        public string ImageFileName { get; } = "unknownCoin.png";

        public string ProofType { get; } = string.Empty;

        public bool IsFullyPremined { get; } = false;

        public string TotalCoinSupply { get; } = "0";

        public string PreMinedValue { get; } = "0";

        public string TotalCoinsFreeFloat { get; } = "0";

        public UnknownFiatCurrency(string currencyCode)
        {
            Code = currencyCode;
            Name = currencyCode;
        }


    }
}