using System;
using System.Diagnostics;
using ChainTicker.DataSource.Coins.DTO;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins.Domain
{

    [DebuggerDisplay("{" + nameof(Description) + "}")]
    internal class Coin : ICoin
    {
        public bool IsValid => true;


        public string Code { get; }

        public string Name { get; }

        public string Description { get; }


        public ICoinUrlSet Urls { get; }
        public IMiningData Mining { get; }


        public Coin(CoinInfo coinInfo, string baseImageUrl, string baseLinkUrl)
        {
            Code = coinInfo.Name;
            Name = coinInfo.CoinName;
            Description = coinInfo.FullName;

            Urls = new CoinUrls(coinInfo.ImageUrl, 
                                                   coinInfo.Url, 
                                                   baseImageUrl + coinInfo.ImageUrl, 
                                                   baseLinkUrl + coinInfo.Url, 
                                                   coinInfo.ImageUrl?.Replace("/", ""));

            Mining = new MiningInfo(Convert.ToBoolean(Convert.ToInt32(coinInfo.FullyPremined)),
                                                    coinInfo.PreMinedValue,
                                                    coinInfo.ProofType,
                                                    coinInfo.TotalCoinsFreeFloat,
                                                    coinInfo.TotalCoinSupply,
                                                    coinInfo.Algorithm);
        }

        
    }
}
