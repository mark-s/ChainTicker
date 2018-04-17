﻿using Microsoft.Practices.Unity;
using Prism.Unity;
using ChainTicker.Shell.Views;
using System.Windows;
using ChainTicker.DataSource.Coins;
using ChainTicker.DataSource.FiatCurrencies;
using ChainTicker.Exchange.BitFlyer;
using ChainTicker.Exchange.Gdax;
using ChainTicker.Transport.Rest;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;

namespace ChainTicker.Shell
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
            container.RegisterType<IChainTickerFileService,ChainTickerFileService>(new ContainerControlledLifetimeManager());
            

        }

        private void RegisterDataSources(IUnityContainer container)
        {
            container.RegisterType<ICoinInfoService, CoinInfoService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFiatCurrenciesService, FiatCurrenciesService>(new ContainerControlledLifetimeManager());
        }

        private void RegisterExchanges(IUnityContainer container)
        {
            container.RegisterType<BitFlyerExchange>(new ContainerControlledLifetimeManager());
            container.RegisterType<GdaxExchange>(new ContainerControlledLifetimeManager());

        }


        protected override DependencyObject CreateShell()
            => Container.Resolve<MainBar>();

        protected override void InitializeShell()
            => Application.Current.MainWindow.Show();
    }
}
