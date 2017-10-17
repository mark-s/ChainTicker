using System;
using System.Diagnostics;
using ChainTicker.DataSource.Coins.DTO;
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
        
        public string ImageUrlShort { get; }

        public string InfoUrlShort { get; }

        public string ImageUrlFull { get; }

        public string InfoUrlFull { get; }

        public string ImageFileName { get; }

        public string Algorithm { get; }

        public string ProofType { get; }

        public bool IsFullyPremined { get; }

        public string TotalCoinSupply { get; }

        public string PreMinedValue { get; }

        public string TotalCoinsFreeFloat { get; }


        public Coin(CoinInfo coinInfo, string baseImageUrl, string baseLinkUrl)
        {

            Code = coinInfo.Name;
            Name = coinInfo.CoinName;
            Description = coinInfo.FullName;

            InfoUrlFull = baseLinkUrl + coinInfo.Url;
            ImageUrlFull = baseImageUrl + coinInfo.ImageUrl;

            InfoUrlShort = coinInfo.Url;
            ImageUrlShort = coinInfo.ImageUrl;

            ImageFileName = coinInfo.ImageUrl?.Replace("/", "");

            Algorithm = coinInfo.Algorithm;
            ProofType = coinInfo.ProofType;
            IsFullyPremined = Convert.ToBoolean(Convert.ToInt32( coinInfo.FullyPremined));

            TotalCoinSupply = coinInfo.TotalCoinSupply;
            PreMinedValue = coinInfo.PreMinedValue;
            TotalCoinsFreeFloat = coinInfo.TotalCoinsFreeFloat;
        }

        
    }
}
