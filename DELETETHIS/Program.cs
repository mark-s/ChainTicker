using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.IO;

namespace DELETETHIS
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTing();

        }

        private static void RunTing()
        {
            var restService = new RestService();
            var folderService = new FolderService();
            var fileIoService = new FileIOService(folderService);
            var diskCache = new DiskCache(fileIoService);
            var fileService = new ChainTickerFileService(diskCache, fileIoService, new ChainTickerJsonSerializer());

            var exchange = new BitFlyerExchange(restService, fileService);

            var markets = exchange.GetAvailableMarketsAsync().Result;


            var vubb = exchange.SubscribeToTicks(markets[0]).Subscribe(
                                     x => Console.WriteLine("OnNext: {0}", $"{markets[0].Id}: {x.TimeStamp} {x.Price}"),
                                     ex => Console.WriteLine("OnError: {0}", ex.Message),
                                     () => Console.WriteLine("OnCompleted"));


            var vubb2 = exchange.SubscribeToTicks(markets[2]).Subscribe(
                                     x => Console.WriteLine("OnNext: {0}", $"{markets[2].Id}: {x.TimeStamp} {x.Price}"),
                                     ex => Console.WriteLine("OnError: {0}", ex.Message),
                                     () => Console.WriteLine("OnCompleted"));




            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();
           
            //vubb.Dispose();

            exchange.UnsubscribeFromTicks(markets[0]);


            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();

            vubb2.Dispose();

            exchange.Dispose();

        }

    }
}
