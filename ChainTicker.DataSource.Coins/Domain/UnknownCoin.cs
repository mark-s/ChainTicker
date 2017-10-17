using System.Diagnostics;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins.Domain
{
    [DebuggerDisplay("{" + nameof(Description) + "}")]
    internal class UnknownCoin : ICoin
    {
        public bool IsValid => false;


        public string Code { get; }

        public string Name { get; }

        public string Description { get; }


        public ICoinUrlSet Urls { get; }

        public IMiningData Mining { get; }


        public UnknownCoin(string coinCode)
        {
            Description = Name = Code = coinCode;

            Urls = new CoinUrlsUnknown();

            Mining = new MiningInfoUnknown();
        }


    }
}