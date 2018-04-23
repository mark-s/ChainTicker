using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins;
using ChainTicker.DataSource.FiatCurrencies;
using ChainTicker.Shell.Models;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Shell.Services
{
    public class ExchangeModelsService
    {
        private readonly IFiatCurrenciesService _fiatCurrenciesService;
        private readonly ICoinInfoService _coinInfoService;
        private readonly IEnumerable<IExchangeFactory> _exchangeFactories;

        public ExchangeModelsService(IFiatCurrenciesService fiatCurrenciesService, ICoinInfoService coinInfoService,
            IEnumerable<IExchangeFactory> exchangeFactories)
        {
            _fiatCurrenciesService = fiatCurrenciesService;
            _coinInfoService = coinInfoService;
            _exchangeFactories = exchangeFactories;
        }

        public async Task<List<ExchangeModel>> GetExchangesAsync()
        {

            var exchanges = new List<IExchange>();

            foreach (var factory in _exchangeFactories)
            {
                exchanges.Add(await factory.GetExchangeAsync());
            }

           return exchanges.Select(e => new ExchangeModel(e, CoinInfoFunc)).ToList();
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
