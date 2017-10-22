using System;
using System.Text.RegularExpressions;
using ChanTicker.Core.Interfaces;
using Newtonsoft.Json;

namespace ChanTicker.Core.IO
{
    public class ChainTickerJsonSerializer : ISerialize
    {


        public T Deserialize<T>(string jsonText) => JsonConvert.DeserializeObject<T>(jsonText);

        public string Serialize<T>(T value) => JsonConvert.SerializeObject(value);
    }
}