/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:ChainTicker.App.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System.Text;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using ChainTicker.App.Models;
using ChainTicker.App.Services;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using ChainTicker.DataSource.Coins;
using ChainTicker.DataSource.FiatCurrencies;
using ChainTicker.Exchange.BitFlyer;
using ChainTicker.Exchange.Gdax;
using ChainTicker.Transport.Rest;

using GalaSoft.MvvmLight;



namespace ChainTicker.App.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                builder.RegisterType<Design.DesignDataService>().As<IDataService>();
            }
            else
            {
                builder.RegisterType<DataService>().As<IDataService>();
            }

            RegisterServices(builder);
            RegisterExchanges(builder);

            var container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }



        private static void RegisterServices(ContainerBuilder builder)
        {

            builder.RegisterType<RestService>().As<IRestService>();
            builder.RegisterType<FolderService>().As<IFolderService>();
            builder.RegisterType<FileIOService>().As<IFileIOService>();

            builder.RegisterType<DiskCache>().As<IDiskCache>();
            builder.RegisterType<ChainTickerJsonSerializer>().As<IJsonSerializer>();
            builder.RegisterType<ChainTickerFileService>().As<IChainTickerFileService>();

            builder.RegisterType<MarketSubscriptionService>().As<IMarketSubscriptionService>();

            builder.RegisterType<CoinInfoService>().As<ICoinInfoService>();
            builder.RegisterType<FiatCurrenciesService>().As<IFiatCurrenciesService>();
        }

        private static void RegisterExchanges(ContainerBuilder builder)
        {

            builder.RegisterType<BitFlyerExchangeFactory>().Named<IExchangeFactory>(nameof(BitFlyerExchangeFactory));
            builder.RegisterType<GdaxExchangeFactory>().Named<IExchangeFactory>(nameof(GdaxExchangeFactory));





            container.RegisterType<ExchangeModelsFactory>(new InjectionConstructor(
                container.Resolve<IFiatCurrenciesService>(),
                container.Resolve<ICoinInfoService>(),
                new ResolvedArrayParameter<IExchangeFactory>(container.ResolveAll<IExchangeFactory>().ToArray())));
        }



        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}