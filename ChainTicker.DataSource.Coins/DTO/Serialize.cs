using Newtonsoft.Json;

namespace ChainTicker.DataSource.Coins.DTO
{
    public static class Serialize
    {
        public static string ToJson(this AllCoinsResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}