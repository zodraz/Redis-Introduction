using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisIntroduction.Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
                IDatabase db = redis.GetDatabase();
                Console.WriteLine("---------Adding countries---------");
                RedisValue[] countries = new RedisValue[] { "Spain", "France", "Italy", "USA" };
                db.ListLeftPush("countries", countries);
                var rv = db.ListRange("countries", 0, -1);
                foreach (var val in rv)
                {
                    Console.WriteLine(val);
                }
                Console.ReadKey();
                Console.WriteLine("---------Adding Colombia---------");
                db.ListRightPush("countries", "Colombia");
                rv = db.ListRange("countries", 0, -1);
                foreach (var val in rv)
                {
                    Console.WriteLine(val);
                }
                Console.ReadKey();
                Console.WriteLine("---------Trimming elements---------");
                db.ListTrim("countries", 0, 2);
                rv = db.ListRange("countries", 0, -1);
                foreach (var val in rv)
                {
                    Console.WriteLine(val);
                }
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
