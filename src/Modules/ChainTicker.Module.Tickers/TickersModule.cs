using System.Collections.Generic;
using ChainTicker.Module.Tickers.Services;
using ChainTicker.Module.Tickers.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace ChainTicker.Module.Tickers
{
    public class TickersModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private IUnityContainer _container;

        public TickersModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<MainBar>();
            RegisterServices();
            RegisterExchanges();
        }

        private void RegisterServices()
        {



            _container.RegisterType<MarketSubscriptionService, IMarketSubscriptionService>();

            _container.RegisterType<CoinsService, ICoinsService>();
            _container.RegisterType<FiatCurrenciesService, IFiatCurrenciesService>();

            _container.RegisterType<ChainTickerJsonSerializer, IJsonSerializer>();
            _container.RegisterType<RandomUserAgentService>();


        }


        }


    }
}