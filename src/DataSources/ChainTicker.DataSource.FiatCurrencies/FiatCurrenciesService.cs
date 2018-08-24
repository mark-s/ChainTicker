using System.Collections.Generic;
using ChainTicker.DataSource.FiatCurrencies.Domain;
using ChainTicker.DataSource.FiatCurrencies.Properties;
using ChainTicker.Core.Interfaces;
using ChainTicker.DataSource.FiatCurrencies.DTOs;

namespace ChainTicker.DataSource.FiatCurrencies
{
    public class FiatCurrenciesService : IFiatCurrenciesService
    {
        // code - Coin
        private readonly Dictionary<string, ICoin> _fiatCurrencies;


        public FiatCurrenciesService(IJsonSerializer jsonSerializer)
        {
            var currencies = jsonSerializer.Deserialize<List<FiatCurrencyDto>>(Resources.FiatCurrencies);

            _fiatCurrencies = ConvertAllToCoins(currencies);
        }

        private Dictionary<string, ICoin> ConvertAllToCoins(IReadOnlyCollection<FiatCurrencyDto> currencies)
        {
            var coinsToReturn = new Dictionary<string, ICoin>(currencies.Count);

            foreach (var fiatCurrency in currencies)
                coinsToReturn[fiatCurrency.Code] = new FiatCurrency(fiatCurrency);

            return coinsToReturn;
        }

        public ICoin GetCurrencyInfo(string currencyCode)
        {
            if (_fiatCurrencies.TryGetValue(currencyCode, out var currency))
                return currency;
            else
                return new UnknownFiatCurrency(currencyCode);
        }
    }
}