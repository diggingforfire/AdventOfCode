using System;
using System.IO;
using System.Linq;

namespace _06._01
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new bool[1000, 1000];
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

                if (parts[0] == "turn")
                {
                    bool value = parts[1] == "on";
                    Enumerable.Range(x1, x2 - x1 + 1).ToList().ForEach(x => Enumerable.Range(y1, y2 - y1 + 1).ToList().ForEach(y => { grid[x,y] = value ; }));
                }
                else
                    Enumerable.Range(x1, x2 - x1 + 1).ToList().ForEach(x => Enumerable.Range(y1, y2 - y1 + 1).ToList().ForEach(y => { grid[x, y] = !grid[x,y];}));
            }


            int count = grid.Cast<bool>().Sum(b => b ? 1 : 0);
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}