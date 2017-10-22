using ChanTicker.Core.IO;
using NUnit.Framework;
using Shouldly;

namespace ChanTicker.Core.Tests.IO
{
    [TestFixture]
    public class JsonHelpersTests
    {

        [Test]
        public void GetMessageType_Valid_ReturnsTypeString()
        {
            // Arrange
            var json = "{\"type\":\"ticker\",\"sequence\":1336278360,\"product_id\":\"ETH-USD\",\"price\":\"298.55000000\",\"open_24h\":\"299.61000000\",\"volume_24h\":\"103504.26315607\",\"low_24h\":\"298.55000000\",\"high_24h\":\"306.55000000\",\"volume_30d\":\"2916693.04560792\",\"best_bid\":\"298.54\",\"best_ask\":\"298.55\"}";

            // Act
            var result = JsonHelpers.GetType(json);

            // Assert
            result.ShouldBe("ticker");
        }

    }
}
