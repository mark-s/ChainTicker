using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChainTicker.Ui.Models;

namespace ChainTicker.Ui.Controls
{
    /// <summary>
    /// Interaction logic for MarketsSelector.xaml
    /// </summary>
    public partial class MarketsSelector : UserControl
    {
        public MarketsSelector()
        {
            InitializeComponent();

        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((FrameworkElement)e.OriginalSource).DataContext is MarketModel market)
            {
                market.IsSubscribed = !market.IsSubscribed;
            }
        }
    }
}
