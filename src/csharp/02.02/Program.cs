using System;
using System.IO;
using System.Linq;

namespace _02._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var presents = lines.Select(line =>
            {
                var parts = line.Split('x');
                var p = new { Length = int.Parse(parts[0]), Width = int.Parse(parts[1]), Height = int.Parse(parts[2]) };
                return new { p.Length, p.Width, p.Height, Cubic = p.Length * p.Width * p.Height };
            });

            int total = presents.Sum(p => new[] {p.Length, p.Width, p.Height}.OrderBy(i => i).Take(2).Sum(i => 2*i) + p.Cubic);

            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}