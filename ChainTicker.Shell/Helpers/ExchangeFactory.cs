using System.Collections.Generic;
using ChainTicker.Exchange.BitFlyer;
using ChainTicker.Exchange.Gdax;
using ChanTicker.Core.Interfaces;
using Microsoft.Practices.Unity;

namespace ChainTicker.Shell.Helpers
{
    public class ExchangeFactory
    {
        private readonly IUnityContainer _container;

        public ExchangeFactory(IUnityContainer container)
        {
            _container = container;
        }

        public List<IExchange> GetExchanges()
        {
            return new List<IExchange>
                       {
                           _container.Resolve<BitFlyerExchange>(),
                           _container.Resolve<GdaxExchange>()
                       };
        }


    }
}
