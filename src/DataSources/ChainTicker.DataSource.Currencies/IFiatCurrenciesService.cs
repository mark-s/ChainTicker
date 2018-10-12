using ChainTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Currencies
{
    public interface IFiatCurrenciesService
    {
        ICoin GetCurrencyInfo(string currencyCode);
    }
}

