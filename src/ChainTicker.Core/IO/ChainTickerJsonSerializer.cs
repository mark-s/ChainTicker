using ChainTicker.Core.Interfaces;
using Newtonsoft.Json;

namespace ChainTicker.Core.IO
{
    public class ChainTickerJsonSerializer : IJsonSerializer
    {

        public T Deserialize<T>(string jsonText) => JsonConvert.DeserializeObject<T>(jsonText);

        public string Serialize<T>(T value) => JsonConvert.SerializeObject(value);

    }
}