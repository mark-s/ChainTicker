using System.Diagnostics;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins.Domain
{
    [DebuggerDisplay("{" + nameof(Description) + "}")]
    internal sealed class UnknownCoin : CoinBase, ICoin
    {
        internal UnknownCoin(string coinCode)
        {
            IsValid = false;

            Description = Name = Code = coinCode;
        }
    }
}