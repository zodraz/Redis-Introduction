using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisIntroduction.Transactions1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
                IDatabase db = redis.GetDatabase();
                Console.WriteLine("---------John account value?---------");
                var res = db.StringGet("account-john");
                Console.WriteLine("John account is: " + res);
                Console.ReadKey();
                Console.WriteLine("---------Starting transaction adding 50 to john account---------");
                var tran = db.CreateTransaction();
                tran.AddCondition(Condition.StringEqual("account-john", 50));
                tran.StringIncrementAsync("account-john", 50);
                Console.WriteLine("About to execute John account incrementint by 50. Press key");
                Console.ReadKey();
                string committed = tran.Execute() ? "committed": "rollback";
                Console.WriteLine("Transaction was " + committed);
                Console.ReadKey();
                Console.WriteLine("---------John account value?---------");
                res = db.StringGet("account-john");
                Console.WriteLine("John account is: " + res);
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
