using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _16._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var equal = new Dictionary<string, int> { { "children", 3 }, { "samoyeds", 2 },  { "akitas", 0 },{ "vizslas", 0 },  { "cars", 2 }, { "perfumes", 1 }};
            var more = new Dictionary<string, int> { { "cats", 7 }, { "trees", 3 }};
            var fewer = new Dictionary<string, int> { { "pomeranians", 3 }, { "goldfish", 5 }};

            var sue = lines
                .Select((l, i) => l.Substring(l.IndexOf(':') + 2, l.Length - l.IndexOf(':') - 2).Split(','))
                .Select((l, i) => new { Index = ++i, Props = l.Select(p => p.Split(':')).ToDictionary(kvp => kvp[0].Trim(), kvp => Int32.Parse(kvp[1])) })
                .Single(item => item.Props.All(kvp => 
                    equal.ContainsKey(kvp.Key) && kvp.Value == equal[kvp.Key] ||
                    more.ContainsKey(kvp.Key) && kvp.Value > more[kvp.Key] ||
                    fewer.ContainsKey(kvp.Key) && kvp.Value < fewer[kvp.Key]
                    ));

            Console.WriteLine(sue.Index);
            Console.ReadKey();
        }
    }
}