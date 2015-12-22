using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _13._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var seatings = File.ReadAllLines("input.txt")
                .Select(line => line.Split(' '))
                .Select(parts => new { From = parts[0], To = parts[parts.Length - 1].Trim('.'), Score = int.Parse(parts[3]) * (parts[2] == "gain" ? 1 : -1) })
                .ToList();

            var people = seatings.Select(s => s.From).Concat(seatings.Select(s => s.To)).Distinct().ToList();

            string me = "Robbie";

            foreach (var person in people)
            {
                seatings.Add(new {From = person, To = me, Score = 0});
                seatings.Add(new {From = me, To = person, Score = 0 });
            }

            people.Add(me);

            var permutations = GetPermutations(people);

            var getGains = new Func<string, string, int>((from, to) => seatings.FirstOrDefault(s => s.From == from && s.To == to).Score);

            var max = permutations.Max(list => list
                .Select((person, i) => new
                {
                    Left = list[i == 0 ? list.Count - 1 : i - 1],
                    Center = person,
                    Right = list[i == list.Count - 1 ? 0 : i + 1]
                }).Sum(p => getGains(p.Center, p.Left) + getGains(p.Center, p.Right)));

            Console.WriteLine(max);
            Console.ReadKey();
        }

        static List<List<string>> GetPermutations(List<string> people)
        {
            var permutations = new List<List<string>>();

            foreach (var person in people)
                foreach (var perms in GetPermutations(people.Except(new[] { person }).ToList()))
                {
                    perms.Insert(0, person);
                    permutations.Add(perms);
                }

            if (!permutations.Any())
                permutations.Add(new List<string>());

            return permutations;
        }
    }
}