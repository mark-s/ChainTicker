using System.Linq;
using System.Windows;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using ChainTicker.DataSource.Coins;
using ChainTicker.DataSource.FiatCurrencies;
using ChainTicker.Exchange.BitFlyer;
using ChainTicker.Exchange.Gdax;
using ChainTicker.Transport.Rest;
using ChainTicker.Ui.Services;
using ChainTicker.Ui.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Unity;

namespace ChainTicker.Ui
{
    internal class Bootstrapper : UnityBootstrapper
    {

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            RegisterServices(Container);

            RegisterDataSources(Container);

            RegisterExchanges(Container);
        }

        private void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IRestService, RestService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFolderService, FolderService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFileIOService, FileIOService>(new ContainerControlledLifetimeManager());

            container.RegisterType<IDiskCache, DiskCache>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJsonSerializer, ChainTickerJsonSerializer>(new ContainerControlledLifetimeManager());
            container.RegisterType<IChainTickerFileService, ChainTickerFileService>(new ContainerControlledLifetimeManager());

            container.RegisterType<IMarketSubscriptionService, MarketSubscriptionService>(new ContainerControlledLifetimeManager());

            
        }

        private void RegisterExchanges(IUnityContainer container)
        {
            container.RegisterType<IExchangeFactory, BitFlyerExchangeFactory>(nameof(BitFlyerExchangeFactory), new ContainerControlledLifetimeManager());
            container.RegisterType<IExchangeFactory, GdaxExchangeFactory>(nameof(GdaxExchangeFactory), new ContainerControlledLifetimeManager());

            container.RegisterType<ExchangeModelsFactory>(new InjectionConstructor(
                container.Resolve<IFiatCurrenciesService>(),
                container.Resolve<ICoinInfoService>(),
                container.Resolve<IEventAggregator>(),
                new ResolvedArrayParameter<IExchangeFactory>(container.ResolveAll<IExchangeFactory>().ToArray())));
        }


        private void RegisterDataSources(IUnityContainer container)
        {
            container.RegisterType<ICoinInfoService, CoinInfoService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFiatCurrenciesService, FiatCurrenciesService>(new ContainerControlledLifetimeManager());
        }


        protected override DependencyObject CreateShell()
            => Container.Resolve<MainBar>();

        protected override void InitializeShell()
            => Application.Current.MainWindow.Show();
    }
}
