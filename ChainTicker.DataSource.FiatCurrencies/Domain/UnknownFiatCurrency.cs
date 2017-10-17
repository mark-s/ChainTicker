using System.Diagnostics;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.FiatCurrencies.Domain
{
    [DebuggerDisplay("{" + nameof(Description) + "}")]
    public class UnknownFiatCurrency : ICoin
    {
        public bool IsValid => false;

        public string Code { get; }
        public string Description { get; }
        public string Name { get; }

        public ICoinUrlSet Urls { get; }
        public IMiningData Mining { get; }

        public UnknownFiatCurrency(string currencyCode)
        {
            Code = currencyCode;
            Name = currencyCode;
            Description = currencyCode;

            Urls = new CoinUrlsUnknown();
            Mining = new MiningInfoUnknown();
        }


    }
}