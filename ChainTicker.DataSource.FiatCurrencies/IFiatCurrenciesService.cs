using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.FiatCurrencies
{
    public interface IFiatCurrenciesService
    {
        ICoin GetCurrencyInfo(string currencyCode);
    }
}

