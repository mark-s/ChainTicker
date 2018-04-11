using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins;
using ChainTicker.Shell.Models;
using ChainTicker.Shell.Services;
using Prism.Commands;
using Prism.Mvvm;


namespace ChainTicker.Shell.ViewModels
{
    public class MainBarViewModel : BindableBase
    {
        private readonly ICoinInfoService _coinInfoService;
        private readonly ExchangesService _exchangesService;

        public DelegateCommand InitDataCommand { get; }

        
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }



        private ExchangeCollectionModel _availableExchanges;
        public ExchangeCollectionModel AvailableExchanges
        {
            get => _availableExchanges;
            set => SetProperty(ref _availableExchanges, value);
        }




        public MainBarViewModel(ICoinInfoService coinInfoService, ExchangesService exchangesService)
        {
            _coinInfoService = coinInfoService;
            _exchangesService = exchangesService;

            InitDataCommand = GetInitDataCommand();
            
        }


        private async Task GetCoinsAsync()
            => await _coinInfoService.GetAvailableCoinsAsync();


        private async Task GetExchangesAsync()
        {
            var exchanges = _exchangesService.GetExchanges();
            await _exchangesService.GetMarketsAsync();

            AvailableExchanges = new ExchangeCollectionModel("AvailableExchanges", new ObservableCollection<ExchangeModel>(exchanges));
        }


        private DelegateCommand GetInitDataCommand()
        {
            return new DelegateCommand(async () =>
                                    {
                                            IsLoading = true;

                                            try
                                            {
                                                await GetCoinsAsync();

                                                await GetExchangesAsync();
                                            }
                                            catch (System.Exception ex)
                                            {
                                                Debug.WriteLine(ex.Message);
                                            }
                                            finally
                                            {
                                                IsLoading = false;
                                            }
                                        });
        }

    }



}
