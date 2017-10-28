using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins;
using ChainTicker.DataSource.FiatCurrencies;
using ChainTicker.Shell.Helpers;
using ChainTicker.Shell.Models;
using ChanTicker.Core.Interfaces;
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
