using System.Collections.Generic;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using ChainTicker.Core.Services;
using ChainTicker.Transport.Rest;
using Microsoft.Practices.Unity;

namespace ChainTicker.Shell
{
    public static class UnityExtensions
    {
        public static IUnityContainer AddServices(this IUnityContainer container)
        {
            container.RegisterType<IRestService, RestService>();
            container.RegisterType<IFolderService, FolderService>();
            container.RegisterType<IDiskIOService, DiskIOService>();
            container.RegisterType<ITimeService, TimeService>();
            container.RegisterType<IDiskCache, DiskCache>();
            container.RegisterType<IJsonSerializer, ChainTickerJsonSerializer>();
            container.RegisterType<IChainTickerFileService, ChainTickerFileService>();

            return container;
        }

        public static IUnityContainer AddExchanges(this IUnityContainer container)
        {

            container.RegisterType<IExchangeFactory, BitFlyerExchangeFactory>()
                .Named<>(nameof(BitFlyerExchangeFactory));
            container.RegisterType<GdaxExchangeFactory>().Named<IExchangeFactory>(nameof(GdaxExchangeFactory));

            _container.Register(c => new ExchangeModelsFactory(c.Resolve<IFiatCurrenciesService>(),
                c.Resolve<ICoinsService>(),
                c.Resolve<IEnumerable<IExchangeFactory>>()));

            return container;



        }
    }
}
