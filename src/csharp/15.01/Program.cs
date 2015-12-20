using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _15._01
{
    class Program
    {
        static void Main(string[] args)
        {
            var ingredients = File.ReadAllLines("input.txt")
                .Select(l => l.Split(':'))
                .Select(p => new {Name = p[0], Parts = p[1].Split(',')})
                .Select(p => new {p.Name, Parts = p.Parts.Select(pp => pp.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).ToList()})
                .Select(p => new { p.Name, Capacity = int.Parse(p.Parts[0][1]), Durability = int.Parse(p.Parts[1][1]), Flavor = int.Parse(p.Parts[2][1]), Texture = int.Parse(p.Parts[3][1]), Calories = int.Parse(p.Parts[4][1]) }).ToList();

            var lists = new List<List<int>>();

            // Improvement: solve recursively to make level of nesting dynamic

            for (int i = 0; i <= 100; i++)
                for (int j = 0; j <= 100 - i; j++)
                    for (int k = 0; k <= 100 - j - i; k++)
                        lists.Add(new List<int>(new[] { i, j, k, 100 - k - j - i }));
         
            List<int> scores = new List<int>();

            foreach (var list in lists)
            {
                int capacity, durability, flavor, texture;
                capacity = durability = flavor = texture = 0;

                for (int i = 0; i < 4; i++)
                {
                    capacity += ingredients[i].Capacity*list[i];
                    durability += ingredients[i].Durability * list[i];
                    flavor += ingredients[i].Flavor * list[i];
                    texture += ingredients[i].Texture * list[i];
                }

                if (capacity > 0 && durability > 0 && flavor > 0 && texture > 0)
                {
                    var score = capacity * durability * flavor * texture;
                    scores.Add(score);
                }
       
            }

            Console.WriteLine(scores.Max());
            Console.ReadKey();
        }
    }
}