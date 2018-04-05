using ChanTicker.Core.Interfaces;
using Newtonsoft.Json;

namespace ChanTicker.Core.IO
{
    public class ChainTickerJsonSerializer : ISerialize
    {

        public T Deserialize<T>(string jsonText)
        {
            var rrr = JsonConvert.DeserializeObject<T>(jsonText);
            return rrr;
        }

        public string Serialize<T>(T value) => JsonConvert.SerializeObject(value);
    }
}