using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using ChainTicker.Exchange.BitFlyer.Services;
using ChainTicker.Transport.Rest;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace ChainTicker.Exchange.BitFlyer.Tests
{
    [TestFixture]
    public class MarketsServiceTests
    {
        private IRestService _fakeRestService;
        private IChainTickerFileService _fakeFileService;
        private MarketsService _marketsService;
        private IMarketsServiceCache _fakeCacheService;


        [SetUp]
        public void SetUp()
        {
            _fakeRestService = A.Fake<IRestService>();
            _fakeFileService = A.Fake<IChainTickerFileService>();
            _fakeCacheService = A.Fake<IMarketsServiceCache>();

            var apiConfig = new ApiEndpointCollection
            {
                [ApiEndpointType.Rest] = "https://api.bitflyer.jp",
                [ApiEndpointType.Pubnub] = "sub-c-52a9ab50-29b-e5-baaa-069f8945a4f"
            };

            _marketsService = new MarketsService(apiConfig, _fakeRestService, _fakeCacheService);
        }



        [Test]
        public async Task GetAvailableMarketsAsync_CacheMiss_CallsBitFlyerApi()
        {
            // Arrange
            A.CallTo(() => _fakeFileService.IsCacheStale(A<CachedFile>.Ignored)).Returns(true);
            string rawFromApiCall;
            using (var client = new WebClient())
            {
                rawFromApiCall = client.DownloadString("https://api.bitflyer.jp/v1/getprices");
            }
            var expectedProductCount = Regex.Matches(rawFromApiCall, "product_code").Count;


            // Act 
            var result = await _marketsService.GetAvailableMarketsAsync();


            // Assert
            result.Markets.Count.ShouldBe(expectedProductCount);
        }



    }
}
