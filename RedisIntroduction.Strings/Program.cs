using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisIntroduction.Strings
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
                IDatabase db = redis.GetDatabase();
                Console.WriteLine("---------Adding and retriving user---------");
                var value = "John";
                db.StringSet("user", value);
                db.StringGet("user");
                Console.WriteLine(value);
                Console.ReadKey();
                Console.WriteLine("---------Key 'user' exists?---------");
                var bValue = db.KeyExists("user");
                var exists = bValue ? "exists" : "does not exist";
                Console.WriteLine("'user' key " + exists);
                Console.ReadKey();
                Console.WriteLine("---------Adding and incrementing a counter---------");
                db.StringSet("cnt", 1);
                db.StringIncrement("cnt", 10);
                var rv= db.StringGet("cnt");
                Console.WriteLine("'cnt' key incremented from one to " + rv);
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
