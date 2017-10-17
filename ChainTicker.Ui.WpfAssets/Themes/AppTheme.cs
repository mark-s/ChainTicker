using System;
using System.Windows;

namespace ChainTicker.Ui.WpfAssets.Themes
{
    public class AppTheme
    {
        public static readonly ResourceKey ColoursSourceKey = new ComponentResourceKey(typeof(Uri), "ColoursSource");

        public static void Set(bool redAndGreenIsAsian = false)
        {
            if (redAndGreenIsAsian)
                SetForAsian();
            else
                SetForNonAsian();
        }

        private static void SetForNonAsian() 
            => Application.Current.Resources.Add(ColoursSourceKey, GetColoursUri("Western"));

        private static void SetForAsian()
            => Application.Current.Resources.Add(ColoursSourceKey, GetColoursUri("Asian"));

        private static Uri GetColoursUri(string theme)
            => new Uri($"pack://application:,,,/ChainTicker.Ui.WpfAssets;component/Themes/{theme}Colours.xaml");
    }
}
