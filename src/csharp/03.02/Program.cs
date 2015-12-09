using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace _03._02
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");

            var points = new[] {new Point(0, 0), new Point(0, 0)};
            int x = 0;
            int y = 0;
            int roboX = 0;
            int roboY = 0;

           
            var getX = new Func<char, int, int>((c, i) =>
            {
                int offset = c == '<' ? -1 : (c == '>' ? 1 : 0);
                if (i%2 == 0)
                    return x += offset;
                return roboX += offset;
            });

            var getY = new Func<char, int, int>((c, i) =>
            {
                int offset = c == 'v' ? -1 : (c == '^' ? 1 : 0);
                if (i % 2 == 0)
                    return y += offset;
                return roboY += offset;
            });

            var houses = input.Select( (c, i) => new { X = getX(c, i), Y = getY(c, i) }).ToList();
            int result = houses.Distinct().Count();

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
