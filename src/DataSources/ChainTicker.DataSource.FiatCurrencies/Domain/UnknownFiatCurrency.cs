using System.Diagnostics;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.DataSource.FiatCurrencies.Domain
{
    [DebuggerDisplay("{" + nameof(Description) + "}")]
    public class UnknownFiatCurrency : ICoin
    {
        public bool IsValid => false;

        public string Code { get; }
        public string Description { get; }
        public string Name { get; }


        internal UnknownFiatCurrency(string currencyCode)
        {
            Code = currencyCode;
            Name = currencyCode;
            Description = currencyCode;
        }


    }
}