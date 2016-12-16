using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisIntroduction.Geo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var clientsManager = new PooledRedisClientManager("127.0.0.1");

                using (var redis = clientsManager.GetClient())
                {
                    Console.WriteLine("---------Adding cities---------");
                    redis.AddGeoMember("Spain", 41.3933691, 2.17884, "Barcelona");
                    redis.AddGeoMember("Spain", 41.9803112, 2.7836476, "Girona");
                    redis.AddGeoMember("Spain", 41.9208051, 2.1562139, "Vic");
                    redis.AddGeoMember("Spain", 41.9716136, 2.7696955, "Salt");

                    var cities = redis.GetGeoCoordinates("Spain", "Barcelona", "Girona", "Vic");

                    foreach (var city in cities)
                    {
                        Console.WriteLine("City: " + city.Member + ", Latitude: " + city.Latitude + ", Longitude: " + city.Longitude);
                    }
                    Console.ReadKey();
                    Console.WriteLine("---------Get distance amongst Barcelona and Girona---------");
                    var distance = redis.CalculateDistanceBetweenGeoMembers("Spain", "Barcelona", "Girona", "km");
                    Console.WriteLine("Distance amongst Barcelona and Girona is " + distance + " km");
                    Console.ReadKey();
                    Console.WriteLine("--------Find cities in a radius of 50km from point 41.9235973 / 1.7187788,---------");
                    var citiesInRadius = redis.FindGeoMembersInRadius("Spain", 41.9235973, 1.7187788, 50, "km");
                    foreach (var city in citiesInRadius)
                    {
                        Console.WriteLine("City: " + city);
                    }

                    Console.WriteLine("--------Find cities in a radius of 100km from Girona--------");
                    var citiesInRadiusWithGeo = redis.FindGeoResultsInRadius("Spain", "Girona", 100, "km");
                    foreach (var city in citiesInRadiusWithGeo)
                    {
                        Console.WriteLine("City: " + city.Member + ", Latitude: " + city.Latitude + ", Longitude: " + city.Longitude);

                    }
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }
}
