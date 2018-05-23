using ChainTicker.Core.Interfaces;

namespace ChainTicker.DataSource.FiatCurrencies
{
    public interface IFiatCurrenciesService
    {
        ICoin GetCurrencyInfo(string currencyCode);
    }
}

