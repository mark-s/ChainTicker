using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace ChainTicker.Ui.Models
{
    public class ExchangeCollectionModel : BindableBase
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