using System.Collections.Generic;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.DataSource.FiatCurrencies.Domain
{
    internal class FiatCurrenciesCollection
    {
        // code - Coin
        private readonly Dictionary<string, ICoin> _fiatCurrencies;

        public FiatCurrenciesCollection(int initialSize)
        {
            _fiatCurrencies = new Dictionary<string, ICoin>(initialSize);
        }

        internal void Add(IEnumerable<ICoin> fiatCurrencies)
        {
            foreach (var currency in fiatCurrencies)
                _fiatCurrencies[currency.Code] = currency;
        }


        internal ICoin GetCurrencyInfo(string currencyCode)
        {
            if (_fiatCurrencies.TryGetValue(currencyCode, out var currency))
                return currency;
            else
            {
                _fiatCurrencies[currencyCode] = new UnknownFiatCurrency(currencyCode);
                return _fiatCurrencies[currencyCode];
            }
        }


    }
}