using System.Diagnostics;
using ChainTicker.Core.Interfaces;
using ChainTicker.DataSource.Currencies.DTOs;

namespace ChainTicker.DataSource.Currencies.Domain
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
