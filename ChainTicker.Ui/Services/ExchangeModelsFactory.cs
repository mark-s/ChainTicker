using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;
using ChainTicker.DataSource.Coins;
using ChainTicker.DataSource.FiatCurrencies;
using ChainTicker.Ui.Models;
using Prism.Events;

namespace ChainTicker.Ui.Services
{
    public class ExchangeModelsFactory
    {
        private readonly IFiatCurrenciesService _fiatCurrenciesService;
        private readonly ICoinInfoService _coinInfoService;
        private readonly IEnumerable<IExchangeFactory> _exchangeFactories;
        private readonly IEventAggregator _eventAggregator;

        public ExchangeModelsFactory(IFiatCurrenciesService fiatCurrenciesService,
                                                        ICoinInfoService coinInfoService,
                                                        IEventAggregator eventAggregator,
                                                        IEnumerable<IExchangeFactory> exchangeFactories)
        {
            _fiatCurrenciesService = fiatCurrenciesService;
            _coinInfoService = coinInfoService;
            _exchangeFactories = exchangeFactories;
            _eventAggregator = eventAggregator;
        }

        public async Task<ExchangeCollectionModel> GetExchangesAsync()
        {

            var exchangeModels = new List<ExchangeModel>();

            foreach (var factory in _exchangeFactories)
            {
                var exchange = await factory.GetExchangeAsync();
                exchangeModels.Add(new ExchangeModel(exchange, CoinInfoFunc, _eventAggregator));
            }

            return new ExchangeCollectionModel("AvailableExchanges", new ObservableCollection<ExchangeModel>(exchangeModels));
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
