using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using ChainTicker.Exchange.BitFlyer;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.IO;

namespace ChainTicker.UI.Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitFlyerExchange _exchange;

        public MainWindow()
        {
            InitializeComponent();

            var restService = new RestService();
            var folderService = new FolderService();
            var fileIoService = new FileIOService(folderService);
            var diskCache = new DiskCache(fileIoService);
            var fileService = new ChainTickerFileService(diskCache, fileIoService, new ChainTickerJsonSerializer());

            _exchange = new BitFlyerExchange(restService, fileService);

        }


        private async void btnGetMarkets_Click(object sender, RoutedEventArgs e)
        {
            var markets =  await  _exchange.GetAvailableMarketsAsync();
            listBox.ItemsSource = markets;
        }

        private void btnSubscribe_Click(object sender, RoutedEventArgs e)
        {
            var market = listBox.SelectedItems[0] as Market;
            var vubb = _exchange.SubscribeToTicks(market).
                ObserveOnDispatcher().
                Subscribe(
                                                                       x => ticksBox.Text +=  $"{market.Id}: {x.TimeStamp} {x.Price}" + Environment.NewLine,
                                                                       ex => ticksBox.Text +=  ex.Message,
                                                                       () => ticksBox.Text += "OnCompleted");

            

        }

        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            var market = listBox.SelectedItems[0] as Market;
            _exchange.UnsubscribeFromTicks(market);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _exchange.Dispose();
        }
    }
}
