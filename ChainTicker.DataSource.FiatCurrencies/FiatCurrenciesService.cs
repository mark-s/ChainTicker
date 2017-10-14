using System.Collections.Generic;
using ChainTicker.DataSource.FiatCurrencies.Domain;
using ChainTicker.DataSource.FiatCurrencies.DTO;
using ChainTicker.DataSource.FiatCurrencies.Properties;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.DataSource.FiatCurrencies
{
    public class FiatCurrenciesService : IFiatCurrenciesService
    {
        private readonly ISerialize _serializer = new ChainTickerJsonSerializer();

        // code - Coin
        private readonly Dictionary<string, ICoin> _fiatCurrencies;


        public FiatCurrenciesService()
        {
            var currencies = _serializer.Deserialize<List<FiatCurrency>>(Resources.FiatCurrencies);

            _fiatCurrencies = ConvertAllToCoins(currencies);
        }

        private Dictionary<string, ICoin> ConvertAllToCoins(IReadOnlyCollection<FiatCurrency> currencies)
        {
            var coinsToReturn = new Dictionary<string, ICoin>(currencies.Count);

            foreach (var fiatCurrency in currencies)
                coinsToReturn[fiatCurrency.Code] = new FiatCoin(fiatCurrency);

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