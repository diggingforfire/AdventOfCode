using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02._01
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var presents = lines.Select(line =>
            {
                var parts = line.Split('x');
                var p = new { Length = int.Parse(parts[0]), Width = int.Parse(parts[1]), Height = int.Parse(parts[2])};
                return new {LW = p.Length*p.Width, WH = p.Width*p.Height, LH = p.Length*p.Height};
            });

            int total = presents.Sum(p => 2*p.LW + 2*p.WH + 2*p.LH + new[] {p.LW, p.WH, p.LH}.Min());

            Console.WriteLine(total);
            Console.ReadKey();

        }
    }
}
