using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins;
using ChainTicker.Shell.Models;
using ChainTicker.Shell.Services;

namespace ChainTicker.Shell.ViewModels
{
    public class MainBarViewModel
    {
        private readonly ICoinInfoService _coinInfoService;
        
        public ExchangeCollectionModel AvailableExchanges { get; }


        public MainBarViewModel(ICoinInfoService coinInfoService, ExchangesService exchangesService)
        {
            _coinInfoService = coinInfoService;

            AvailableExchanges = new ExchangeCollectionModel("AvailableExchanges", 
                                                                                   new ObservableCollection<ExchangeModel>(exchangesService.GetExchanges()));

            InitAsync();
        }


        private async Task InitAsync()
        {
            await _coinInfoService.PopulateAvailableCoinsAsync();
        }




    }



}
