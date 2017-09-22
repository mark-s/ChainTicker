using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace ChainTicker.DataSource.Coins.Tests
{

    [TestFixture]
    public class CoinServiceTests
    {

        [Test]
        public async Task GetAllAvailableCoins_ReturnsAllCoinsAsync()
        {

            var cs = new CoinService();
            
            var result = await cs.GetAllAvailableCoinsAsync(null);

            result.Count.ShouldBe(100);

        }


    }
}