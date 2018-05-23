using System;
using System.Globalization;
using System.Windows.Data;

namespace ChainTicker.Ui.Helpers
{
    public class PriceConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tick = value as decimal?;

            return tick.GetValueOrDefault().ToString("#,0.####", CultureInfo.InvariantCulture);

        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}
