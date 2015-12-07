using System;
using System.IO;
using System.Linq;

namespace _03._01
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");

            int x = 0;
            int y = 0;

            var getX = new Func<char, int>((c) => { return x += c == '<' ? -1 : (c == '>' ? 1 : 0); });
            var getY = new Func<char, int>((c) => { return y += c == 'v' ? -1 : (c == '^' ? 1 : 0); });

            var points = input.Select(c => new {X = getX(c), Y = getY(c)}).ToList();
            int result = points.Distinct().Count();

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}