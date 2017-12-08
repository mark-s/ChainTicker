using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins;
using ChainTicker.Shell.Models;
using ChainTicker.Shell.Services;
using Prism.Mvvm;

namespace ChainTicker.Shell.ViewModels
{
    public class MainBarViewModel : BindableBase
    {
        private readonly ICoinInfoService _coinInfoService;
        private readonly ExchangesService _exchangesService;

        public ExchangeCollectionModel AvailableExchanges { get; set; }


        public MainBarViewModel(ICoinInfoService coinInfoService, ExchangesService exchangesService)
        {
            _coinInfoService = coinInfoService;
            _exchangesService = exchangesService;

            AvailableExchanges = new ExchangeCollectionModel("AvailableExchanges", 
                                                                                   new ObservableCollection<ExchangeModel>(exchangesService.GetExchanges()));
            

            InitAsync();
        }


        private async Task InitAsync()
        {
            await _coinInfoService.PopulateAvailableCoinsAsync();
            await _exchangesService.GetMarketsAsync(_exchangesService.GetExchanges());
            
        }




    }



}
