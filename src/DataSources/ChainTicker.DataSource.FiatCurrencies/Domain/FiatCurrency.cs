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

        public ICoinUrlSet Urls { get; }
        
        public FiatCurrency(FiatCurrencyDto fiatCurrencyDto)
        {
            Code = fiatCurrencyDto.Code;
            Description = fiatCurrencyDto.Name;
            Name = fiatCurrencyDto.Symbol;

            // TODO: Figure this out
            Urls = new CoinUrls(fiatCurrencyDto.Image,
                                                        "http://www.xe.com/currency/" + fiatCurrencyDto.Code.ToLowerInvariant(),
                                                        "./Images/" + fiatCurrencyDto.Image,
                                                        "http://www.xe.com/currency/" + fiatCurrencyDto.Code.ToLowerInvariant(),
                                                        "????");

        }





    }
}
