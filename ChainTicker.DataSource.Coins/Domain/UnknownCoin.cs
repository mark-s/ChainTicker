using System.Diagnostics;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins.Domain
{
    [DebuggerDisplay("{" + nameof(Description) + "}")]
    internal sealed class UnknownCoin : CoinBase, ICoin
    {
        public UnknownCoin(string coinCode)
        {
            IsValid = false;

            Description = Name = Code = coinCode;

            Urls = new CoinUrlsUnknown();

            Mining = new MiningInfoUnknown();
        }
    }
}