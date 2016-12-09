using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisIntroduction.Subscriber
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
                Console.WriteLine("---------Subscribing to news channel--------");
                sub.Subscribe("news", (channel, message) => {
                    Console.WriteLine("New message from the channel news: " + message);
                });
                Console.WriteLine("---------Subscribing to topic news channel per country--------");
                sub.Subscribe("news:*", (channel, message) => {
                    Console.WriteLine("New message from the topic channel news per country: " + message);
                });
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
