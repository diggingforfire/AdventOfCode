using System.IO;
using System.Linq;

namespace _05._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
          
            var res = lines.Where(line => line.Skip(1)
                .Select((c, i) => new {l = new string(new[] {lines[0][i], c})})
                .GroupBy(g => g.l)
                .Any(g => g.Count() > 1)).ToList();

            var ress = lines.Select(line =>
            {
                var grp = line.Skip(1)
                    .Select((c, i) => new {Value = new string(new[] {line[i], c}), Index = i})
                    .GroupBy(g => g.Value)
                    .Where(g => g.Count() > 1)
                    .Select(g =>
                    new
                    {
                        Line = line,
                        Pair = g
                    });

                return grp;


            }).Where(g => g.Any());



        }
    }
}
