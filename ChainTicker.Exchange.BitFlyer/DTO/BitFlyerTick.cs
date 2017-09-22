﻿using System;
using ChanTicker.Core.Interfaces;
using Newtonsoft.Json;

namespace ChainTicker.Exchange.BitFlyer.DTO
{

    public partial class BitFlyerTick : ITick
    {
        [JsonProperty("best_bid_size")]
        public double BestBidSize { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset TickTimeStamp { get; set; }

        [JsonProperty("best_ask_size")]
        public double BestAskSize { get; set; }

        [JsonProperty("best_ask")]
        public long BestAsk { get; set; }

        [JsonProperty("best_bid")]
        public long BestBid { get; set; }

        [JsonProperty("product_code")]
        public string ProductCode { get; set; }

        [JsonProperty("ltp")]
        public long LastTradedPrice { get; set; }

        [JsonProperty("tick_id")]
        public long TickId { get; set; }

        [JsonProperty("total_bid_depth")]
        public double TotalBidDepth { get; set; }

        [JsonProperty("total_ask_depth")]
        public double TotalAskDepth { get; set; }

        [JsonProperty("volume")]
        public double Volume { get; set; }

        [JsonProperty("volume_by_product")]
        public double VolumeByProduct { get; set; }


        [JsonIgnore]
        public DateTimeOffset TimeStamp => TickTimeStamp;

        [JsonIgnore]
        public decimal? Price => LastTradedPrice;
    }


    public partial class BitFlyerTick
    {
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.DateTimeOffset,
        };

        public static BitFlyerTick FromJson(string json)
        {
            return JsonConvert.DeserializeObject<BitFlyerTick>(json, _settings);
        }
    }


}
