using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace ChainTicker.Exchange.BitFlyer.Tests
{
    [TestFixture]
    public class BitFlyerExchangeTests
    {
        private IRestService _fakeRestService;
        private IChainTickerFileService _fakeFileService;
        private IPubnubTransport _fakePubNubTransport;
        private IPollingPriceService _fakePollingPriceService;
        private IJsonSerializer _fakeJsonSerialiser;
        private BitFlyerExchange _bfExchange;

        [SetUp]
        public void SetUp()
        {
            _fakeRestService = A.Fake<IRestService>();
            _fakeFileService = A.Fake<IChainTickerFileService>();
            _fakePubNubTransport = A.Fake<IPubnubTransport>();
            _fakePollingPriceService = A.Fake<IPollingPriceService>();
            _fakeJsonSerialiser  = A.Fake<IJsonSerializer>();

            _bfExchange = new BitFlyerExchange(_fakeRestService,
                                                            _fakeFileService,
                                                            _fakePubNubTransport,
                                                            _fakePollingPriceService,
                                                            _fakeJsonSerialiser);

        }

        [Test]
        public void Info_Name_CorrectlyPopulated()
        {
            // Arrange
            // Done in SetUp()

            // Act
            var info = _bfExchange.Info;

            // Assert
            info.Name.ShouldBe("bitFlyer");
        }

        [Test]
        public void Info_HomePageUrl_CorrectlyPopulated()
        {
            // Arrange
            // Done in SetUp()

            // Act
            var info = _bfExchange.Info;

            // Assert
            info.HomePageUrl.ShouldBe("https://bitflyer.jp");
        }

        [Test]
        public void Info_Description_CorrectlyPopulated()
        {
            // Arrange
            // Done in SetUp()

            // Act
            var info = _bfExchange.Info;

            // Assert
            info.Description.ShouldBe("bitFlyer Japan");
        }

        [Test]
        public void Info_IsEnabled_CorrectlyPopulated()
        {
            // Arrange
            // Done in SetUp()

            // Act
            var info = _bfExchange.Info;

            // Assert
            info.IsEnabled.ShouldBeTrue();
        }

        [Test]
        public void Info_RestEndpoint_CorrectlyPopulated()
        {
            // Arrange
            // Done in SetUp()

            // Act
            var endpoints = _bfExchange.Info.ApiEndpoints;
            var restEndpoint = endpoints[ApiEndpointType.Rest];

            // Assert
            restEndpoint.ShouldBe("https://api.bitflyer.jp");
        }

        [Test]
        public void Info_PubNumEndpoint_CorrectlyPopulated()
        {
            // Arrange
            // Done in SetUp()

            // Act
            var endpoints = _bfExchange.Info.ApiEndpoints;
            var pubnubEndpoint = endpoints[ApiEndpointType.Pubnub];

            // Assert
            pubnubEndpoint.ShouldBe("sub-c-52a9ab50-29b-e5-baaa-069f8945a4f");
        }

    }
}
