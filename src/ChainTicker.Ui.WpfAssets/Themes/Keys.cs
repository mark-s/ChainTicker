using System.Windows;
using System.Windows.Media;

namespace ChainTicker.Ui.WpfAssets.Themes
{
    public class Keys
    {
        private static ResourceKey CreateKey<T>(string keyId)
        {
            return new ComponentResourceKey(typeof(T), keyId);
        }

        public static readonly ResourceKey PriceDirectionUpColour = CreateKey<Color>("PriceDirectionUpColour");
        public static readonly ResourceKey PriceDirectionDownColour = CreateKey<Color>("PriceDirectionDownColour");

        public static readonly ResourceKey PriceDirectionUpBrush = CreateKey<Brush>("PriceDirectionUpBrush");
        public static readonly ResourceKey PriceDirectionDownBrush = CreateKey<Brush>("PriceDirectionDownBrush");
    }
}
