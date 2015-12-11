using System;
using System.IO;
using System.Linq;

namespace _05._01
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            int count = lines.Count(line => 
                line.Count("aeiou".Contains) >= 3 &&
                !new[] {"ab", "cd", "pq", "xy"}.Any(line.Contains) &&
                line.Where((c, i) => i != 0 && c == line[i - 1]).Any());

            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
