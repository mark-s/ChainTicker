using System;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Interfaces;
using PubnubApi;


namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange
    {
       

        public ISourceInfo SourceInfo { get; } = new BitFlyerSourceInfo();
        private const string SUBSCRIBE_KEY = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f";
        private const string BASE_URL = "https://api.bitflyer.jp";
        

        public ICoinPriceDataSource CoinPriceData { get; }

        public BitFlyerExchange()
        {
            var availableCommande = GetBitFlyerCommandSet();

            CoinPriceData = new BitFlyerCoinPriceDataSource(new PubnubTransport(SUBSCRIBE_KEY,new DebugLogger()), new RestService(availableCommande));
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
