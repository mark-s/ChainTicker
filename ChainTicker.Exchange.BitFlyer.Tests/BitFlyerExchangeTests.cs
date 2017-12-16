using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.IO;
using NUnit.Framework;
using Shouldly;

namespace ChainTicker.Exchange.BitFlyer.Tests
{
    [TestFixture]
    public class BitFlyerExchangeTests
    {


        [Test]
        public async Task METHODUNDERTEST_PARTUNDERTEST_EXPECTEDRESULT()
        {
            // Arrange
            var restService = new RestService(new RandomUserAgentService());

            var exchange = new BitFlyerExchange(restService,null, new ChainTickerJsonSerializer());

            var markets = await exchange.GetAvailableMarketsAsync();

            markets.Count.ShouldBe(10);


            // Act

            // Assert

        }


        [Test]
        public async Task Price_PARTUNDERTEST_EXPECTEDRESULT()
        {
            // Arrange
            var restService = new RestService(new RandomUserAgentService());

            var exchange = new BitFlyerExchange(restService, null, new ChainTickerJsonSerializer());

            var markets = await exchange.GetAvailableMarketsAsync();

            foreach (var market in markets)
            {
                var cp = await exchange.GetCurrentPriceAsync(market);
                Debug.WriteLine(market.ProductCode + " " + cp.LastTradedPrice);
            }


            // Act

            // Assert

        }


        [Test]
        public async Task CanSubscribe()
        {
            // Arrange
            var restService = new RestService(new RandomUserAgentService());
            var exchange = new BitFlyerExchange(restService, null, new ChainTickerJsonSerializer());

            var markets = await exchange.GetAvailableMarketsAsync();

            // Act

            var sub = exchange.SubscribeToTicks(markets[0]);

            var vubb = sub.Subscribe(
                                                        x => Debug.WriteLine("OnNext: {0}", x),
                                                        ex => Debug.WriteLine("OnError: {0}", ex.Message),
                                                        () => Debug.WriteLine("OnCompleted"));

            vubb.Dispose();
            // Assert

        }





    }
}
