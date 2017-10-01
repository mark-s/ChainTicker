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
            var restService = new RestService();
            var fileService = new ChainTickerFileService(new DiskCache(new FileIOService()), new FileIOService(), new ChainTickerJsonSerializer());

            var exchange = new BitFlyerExchange(restService, fileService);

            var markets = await exchange.GetAvailableMarketsAsync();

            markets.Count.ShouldBe(10);


            // Act

            // Assert

        }







    }
}
