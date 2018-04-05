using ChainTicker.DataSource.Coins;
using ChainTicker.DataSource.FiatCurrencies;
using ChainTicker.Shell.Models;
using Prism.Mvvm;

namespace ChainTicker.Shell.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ICoinInfoService _coinInfoService;
        private readonly IFiatCurrenciesService _fiatCurrenciesService;
        
        public ExchangeCollectionModel AvailableExchanges { get; } 


        public MainWindowViewModel()
        {
            
        }





    }
}
