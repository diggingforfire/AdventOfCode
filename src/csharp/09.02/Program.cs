using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _09._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var routes = lines.Select(l => new { From = l.Split(' ')[0], To = l.Split(' ')[2], Distance = Int32.Parse(l.Split(' ')[4]) }).ToList();
            var cities = routes.Select(r => r.From).Concat(routes.Select(r => r.To)).GroupBy(c => c).Select(g => g.Key).ToList();

            var getDistance = new Func<string, string, int>((c1, c2) =>
            {
                var route = routes.FirstOrDefault(r => r.From == c1 && r.To == c2 || r.From == c2 && r.To == c1);
                return route.Distance;
            });

            List<int> distances = new List<int>();
            var permutations = GetPermutations(cities);

            foreach (var permutation in permutations)
            {
                int i = 0;
                int distance = 0;
                foreach (var toCity in permutation.Skip(1))
                {
                    var fromCity = permutation[i++];
                    distance += getDistance(fromCity, toCity);
                }
                distances.Add(distance);
            }

            Console.WriteLine(distances.Max());
            Console.ReadKey();
        }

        static List<List<string>> GetPermutations(List<string> cities)
        {
            var permutations = new List<List<string>>();

            foreach (var city in cities)
                foreach (var perms in GetPermutations(cities.Except(new[] { city }).ToList()))
                {
                    perms.Insert(0, city);
                    permutations.Add(perms);
                }

            if (!permutations.Any())
                permutations.Add(new List<string>());

            return permutations;
        }
    }
}