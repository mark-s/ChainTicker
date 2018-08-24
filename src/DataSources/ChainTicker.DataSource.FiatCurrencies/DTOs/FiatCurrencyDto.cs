namespace ChainTicker.DataSource.FiatCurrencies.DTOs
{
    public class FiatCurrencyDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string DecimalPlaces { get; set; }

        public string SymbolNative { get; set; }

        public string Image { get; set; }
    }

}