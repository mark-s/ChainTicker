using ChainTicker.Core.Interfaces;
using ChainTicker.DataSource.FiatCurrencies.Domain;
using ChainTicker.DataSource.FiatCurrencies.DTOs;
using ChainTicker.DataSource.FiatCurrencies.Properties;
using System.Collections.Generic;
using System.Linq;

namespace ChainTicker.DataSource.FiatCurrencies
{
    public class FiatCurrenciesService : IFiatCurrenciesService
    {
        private readonly FiatCurrenciesCollection _fiatCurrencies;

        public FiatCurrenciesService(IJsonSerializer jsonSerializer)
        {
            var currencies = jsonSerializer.Deserialize<List<FiatCurrencyDto>>(Resources.FiatCurrencies);

            _fiatCurrencies = new FiatCurrenciesCollection(currencies.Count);
            _fiatCurrencies.Add(ConvertAllToCoins(currencies));
        }

        private IEnumerable<ICoin> ConvertAllToCoins(IEnumerable<FiatCurrencyDto> currencies)
            => currencies.Select(fiatCurrency => new FiatCurrency(fiatCurrency));

        public ICoin GetCurrencyInfo(string currencyCode)
            => _fiatCurrencies.GetCurrencyInfo(currencyCode);

    }
}