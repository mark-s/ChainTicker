using System;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Entities
{
    public class Coin : ICoin
    {
        public string Code { get; }
        public string Name { get; }

        public Coin(string code, string name)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Please provide the code and name!");

            Code = code.ToUpperInvariant();
            Name = name;
        }
    }
}