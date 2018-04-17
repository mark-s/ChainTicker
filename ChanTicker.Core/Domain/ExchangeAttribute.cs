using System;

namespace ChainTicker.Core.Domain
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ExchangeAttribute : Attribute
    {
        public string Name { get; }

        public ExchangeAttribute(string name)
        {
            Name = name;
        }

    }
}
