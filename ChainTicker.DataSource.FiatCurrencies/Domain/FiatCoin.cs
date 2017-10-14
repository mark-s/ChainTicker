using System.Diagnostics;
using ChainTicker.DataSource.FiatCurrencies.DTO;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.FiatCurrencies.Domain
{
    // TODO: Clean this up
    [DebuggerDisplay("{" + nameof(Description) + "}")]
    public class FiatCoin : ICoin
    {
        private readonly FiatCurrency _fiatCurrency;

        public bool IsValid => true;


        public string Code => _fiatCurrency.Code;
        public string Description => _fiatCurrency.Name;
        public string Name => _fiatCurrency.Symbol;

        public string Algorithm => "Fiat";

        public string ImageUrlShort => _fiatCurrency.Image;
        public string ImageUrlFull => "./Images/" + _fiatCurrency.Image;
        public string InfoUrlShort  => _fiatCurrency.Code.ToLowerInvariant();

        public string InfoUrlFull => "http://www.xe.com/currency/" + _fiatCurrency.Code.ToLowerInvariant();
        public string ImageFileName => "TODO";

        public bool IsFullyPremined => false;
        public string PreMinedValue => "N/A";
        public string ProofType => "N/A";
        public string TotalCoinsFreeFloat => "N/A";
        public string TotalCoinSupply => "N/A";



        public FiatCoin(FiatCurrency fiatCurrency)
        {
            _fiatCurrency = fiatCurrency;

        }





    }
}
