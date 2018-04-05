// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var welcome = AllCoinsResponse.FromJson(jsonString);

using Newtonsoft.Json;

namespace ChainTicker.DataSource.Coins.DTO
{
    public partial class AllCoinsResponse
    {
        public static AllCoinsResponse FromJson(string json) => JsonConvert.DeserializeObject<AllCoinsResponse>(json, Converter.Settings);
    }
}
