using System;
using System.IO;
using System.Linq;

namespace _05._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            // I guess we could use a regex. I wonder which is more readable right now..

            var matchingLines = lines
                .Select(line =>
                new
                {
                    Line = line,
                    // select all pairs into a new object
                    Value = line.Skip(1).Select((c, i) => new {Pair = new string(new[] {line[i], c}), Index = i})
                })
                .Select(n => 
                new
                {
                    n.Value,
                    n.Line,
                    // select all pairs that appear at least twice
                    RepeatingPairs = n.Value.GroupBy(g => g.Pair).Where(g => g.Count() >= 2)
                })
                .Select(n =>
                new
                {
                    n.Line,
                    n.Value,
                    n.RepeatingPairs,
                    IndexDifference = n.RepeatingPairs.Select(p => p.Max(g => g.Index) - p.Min(g => g.Index))
                }
                )
                // at least two indices apart (so no overlapping)
                .Where(n => n.IndexDifference.Any(i => i >= 2))
                .Select(n => 
                new
                {
                    n.Line,
                    Value = n.Line.Skip(2).Select((c, i) => new {Tri = new string(new[] {n.Line[i], n.Line[i + 1], c}), Index = i})
                })
                // first and last character of triplet must match
                .Where(n => n.Value.Any(v => v.Tri[0] == v.Tri[2]))
                .ToList();

            Console.WriteLine(matchingLines.Count);
            Console.ReadKey();
        }
    }
}
