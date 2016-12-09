using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisIntroduction.SortedSets
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
                IDatabase db = redis.GetDatabase();
                Console.WriteLine("---------Adding users and scores---------");
                SortedSetEntry[] positions = new SortedSetEntry[]
                {
                    new SortedSetEntry("John Smith",1),
                    new SortedSetEntry("Claire Smith",2),
                    new SortedSetEntry("George Roman",30)
                };

                db.SortedSetAdd("leaderboard", positions);
                var rv = db.SortedSetRangeByRank("leaderboard", 0, -1);
                foreach (var value in rv)
                {
                    Console.WriteLine(value);
                }
                Console.ReadKey();
                Console.WriteLine("---------Get ranks with users and scores---------");
                positions = db.SortedSetRangeByRankWithScores("leaderboard", 0, -1);
                foreach (var position in positions)
                {
                    Console.WriteLine("Name:"  + position.Element + ", Score: " + position.Score);
                }
                Console.ReadKey();
                Console.WriteLine("---------Get George Roman score---------");
                var val = db.SortedSetScore("leaderboard", "George Roman");
                Console.WriteLine("Score for George Roman is: " + val);
                Console.ReadKey();
                Console.WriteLine("---------Get George Roman rank---------");
                val = db.SortedSetRank("leaderboard", "George Roman");
                Console.WriteLine("Rank for George Roman is: " + val);
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
