using System.Diagnostics;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.DataSource.FiatCurrencies.DTOs;

namespace ChainTicker.DataSource.FiatCurrencies.Domain
{
    [DebuggerDisplay("{" + nameof(Description) + "}")]
    public class FiatCurrency : ICoin
    {
        public bool IsValid => true;

        public string Code { get; }
        public string Description { get; }
        public string Name { get; }

        public FiatCurrency(FiatCurrencyDto fiatCurrencyDto)
        {
            Code = fiatCurrencyDto.Code;
            Description = fiatCurrencyDto.Name;
            Name = fiatCurrencyDto.Symbol;
        }





    }
}
