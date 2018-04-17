using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins;
using ChainTicker.DataSource.FiatCurrencies;
using ChainTicker.Exchange.BitFlyer;
using ChainTicker.Exchange.Gdax;
using ChainTicker.Shell.Models;
using ChainTicker.Core.Interfaces;
using Microsoft.Practices.Unity;

namespace ChainTicker.Shell.Services
{
    public class ExchangesService
    {
        private readonly IUnityContainer _container;
        private readonly IFiatCurrenciesService _fiatCurrenciesService;
        private readonly ICoinInfoService _coinInfoService;

        public ExchangesService(IUnityContainer container, IFiatCurrenciesService fiatCurrenciesService, ICoinInfoService coinInfoService)
        {
            _container = container;
            _fiatCurrenciesService = fiatCurrenciesService;
            _coinInfoService = coinInfoService;
        }

        public List<ExchangeModel> GetExchanges()
        {
            var exchanges =  new List<IExchange>
                       {
                           _container.Resolve<BitFlyerExchange>(),
                           _container.Resolve<GdaxExchange>()
                       };

           return exchanges.Select(e => new ExchangeModel(e, CoinInfoFunc)).ToList();
        }

        public async Task GetMarketsAsync(List<ExchangeModel> exchanges)
        {
            foreach (var exchange in exchanges)
            {
                await exchange.GetAvailableMarketsAsync();
            }
        }

        private ICoin CoinInfoFunc(string coinOrCurrencyCode)
        {
            var coin = _coinInfoService.GetCoinInfo(coinOrCurrencyCode);
            if (coin.IsValid)
                return coin;
            else
                return _fiatCurrenciesService.GetCurrencyInfo(coinOrCurrencyCode);
        }

    }
}
