using ChainTicker.DataSource.Coins.DTO;

namespace ChainTicker.DataSource.Coins.Domain
{
    public class CoinDetails
    {

        public string Code { get; }

        public string Name { get; }

        public string Description { get; }


        public string InfoUrl { get;  }

        public string ImageUrl { get;  }


        public string Algorithm { get;  }

        public string ProofType { get;  }

        public string FullyPremined { get;  }

        public string TotalCoinSupply { get;  }

        public string PreMinedValue { get;  }

        public string TotalCoinsFreeFloat { get;  }


        public CoinDetails(CoinInfo coinInfo, string baseImageUrl, string baseLinkUrl)
        {

            Code = coinInfo.Name;
            Name = coinInfo.CoinName;
            Description = coinInfo.FullName;

            InfoUrl = baseLinkUrl + coinInfo.Url;
            ImageUrl = baseImageUrl + coinInfo.ImageUrl;

            Algorithm = coinInfo.Algorithm;
            ProofType = coinInfo.ProofType;
            FullyPremined = coinInfo.FullyPremined;

            TotalCoinSupply = coinInfo.TotalCoinSupply;
            PreMinedValue = coinInfo.PreMinedValue;
            TotalCoinsFreeFloat = coinInfo.TotalCoinsFreeFloat;
        }


    }
}
