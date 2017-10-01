
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;


namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange
    {
        public IExchangeInfo ExchangeInfo { get; } = new BitFlyerExchangeInfo();

        private const string SUBSCRIBE_KEY = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f";
        private const string BASE_URL = "https://api.bitflyer.jp";

        public IMarketDataSource MarketDataSource { get; }

        public BitFlyerExchange()
        {
            //var pubnubTransport = new PubnubTransport(SUBSCRIBE_KEY, new DebugLogger());

            var restService = new RestService(new ChainTickerJsonSerializer());
            restService.RegisterCommands(GetBitFlyerCommandSet());

            MarketDataSource = new MarketDataSourceAsync(null, restService);
        }

        private ServiceCommands GetBitFlyerCommandSet()
        {
            var commandSet = new ServiceCommands(BASE_URL);

            commandSet.AddCommand("getmarkets", new Command("/v1/getmarkets"));
            commandSet.AddCommand("getboard", new Command("/v1/getboard", "product_code"));
            commandSet.AddCommand("getticker", new Command("/v1/getticker", "product_code"));
            commandSet.AddCommand("getexecutions", new Command("/v1/getexecutions", "product_code"));
            commandSet.AddCommand("gethealth", new Command("/v1/gethealth"));
            commandSet.AddCommand("getprices", new Command("/v1/getprices"));

            return commandSet;

        }
    }
}
