using System;
using System.IO;
using System.Linq;

namespace _06._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new int[1000, 1000];
            var lines = File.ReadAllLines("input.txt");

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                var first = parts[parts.Length - 3].Split(',');
                var second = parts[parts.Length - 1].Split(',');

                int x1 = Int32.Parse(first[0]);
                int y1 = Int32.Parse(first[1]);
                int x2 = Int32.Parse(second[0]);
                int y2 = Int32.Parse(second[1]);

                int value = parts[0] == "turn" ? (parts[1] == "on" ? 1 : -1) : 2;
                Enumerable.Range(x1, x2 - x1 + 1).ToList().ForEach(x => Enumerable.Range(y1, y2 - y1 + 1).ToList().ForEach(y => { if (grid[x, y] + value >= 0) grid[x, y] += value; }));
            }

            int count = grid.Cast<int>().Sum();
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}