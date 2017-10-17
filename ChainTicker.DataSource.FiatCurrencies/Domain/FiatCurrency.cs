using System.Diagnostics;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

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
        public IMiningData Mining { get; }
        
        public FiatCurrency(DTO.FiatCurrency fiatCurrency)
        {
            Code = fiatCurrency.Code;
            Description = fiatCurrency.Name;
            Name = fiatCurrency.Symbol;

            // TODO: Figure this out
            Urls = new CoinUrls(fiatCurrency.Image,
                                                        "http://www.xe.com/currency/" + fiatCurrency.Code.ToLowerInvariant(),
                                                        "./Images/" + fiatCurrency.Image,
                                                        "http://www.xe.com/currency/" + fiatCurrency.Code.ToLowerInvariant(),
                                                        "????");

            Mining = new MiningInfoUnknown();

        }





    }
}
