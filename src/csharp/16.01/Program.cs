using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _16._01
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var match = new Dictionary<string, int> 
            { {"children", 3},{"cats", 7},{"samoyeds", 2},{"pomeranians", 3},{"akitas", 0},{"vizslas", 0},{"goldfish", 5},{"trees", 3},{"cars", 2},{"perfumes", 1} };
          
            var sue = lines
                .Select((l, i) => l.Substring(l.IndexOf(':') + 2, l.Length - l.IndexOf(':') - 2).Split(',')) 
                .Select( (l, i) => new { Index = ++i, Props = l.Select(p => p.Split(':')).ToDictionary(kvp => kvp[0].Trim(), kvp => Int32.Parse(kvp[1]))})
                .Single(item => item.Props.All(kvp => match[kvp.Key] == kvp.Value));

            Console.WriteLine(sue.Index);
            Console.ReadKey();
        }
    }
}