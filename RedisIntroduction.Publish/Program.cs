using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisIntroduction.Publish
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
                IDatabase db = redis.GetDatabase();
                ISubscriber sub = redis.GetSubscriber();
                Console.WriteLine("---------Publishing a simple notice on news channel--------");
                sub.Publish("news", "Trump won");
                Console.ReadKey();
                Console.WriteLine("---------Publishing to an inexistent channel--------");
                sub.Publish("news2", "Trump won");
                Console.ReadKey();
                Console.WriteLine("---------Publishing a simple notice on a topic news channel on spain--------");
                sub.Publish("news:spain", "Spain news: Obama visited spain");
                Console.ReadKey();
                Console.WriteLine("---------Publishing a simple notice on a topic news channel on usa--------");
                sub.Publish("news:usa", "USA news: Obama left White House");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }
}
