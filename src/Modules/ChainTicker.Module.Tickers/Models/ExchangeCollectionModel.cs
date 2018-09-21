using System.Collections.ObjectModel;

namespace ChainTicker.Module.Tickers.Models
{
    public class ExchangeCollectionModel
    {
        public string Header { get; }

        public ObservableCollection<ExchangeModel> Exchanges { get; }

        public ExchangeCollectionModel(string header, ObservableCollection<ExchangeModel> exchanges)
        {
            Header = header;
            Exchanges = exchanges;
        }
    }
}