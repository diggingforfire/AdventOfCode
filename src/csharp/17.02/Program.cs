using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17._02
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var containers = File.ReadAllLines("input.txt").Select(int.Parse).ToList();
            var powerSet = PowerSet(containers).Where(p => p.Any()).ToList();

            var filteredsets = powerSet.Where(p => p.Sum() == 150).OrderBy(p => p.Count).ToList();
            var count = filteredsets.TakeWhile((p, i) => p.Count == filteredsets[++i].Count).Count() + 1;

            Console.WriteLine(count);
            Console.ReadKey();
        }

        private static List<List<int>> PowerSet(List<int> set)
        {
            var s = new List<List<int>>();

            if (!set.Any())
            {
                s.Add(new List<int>());
                return s;
            }

            var rest = new List<int>(set);
            rest.RemoveAt(0);
            var res = PowerSet(rest);

            foreach (var r in res)
            {
                s.Add(new List<int>(r));
                r.Insert(0, set[0]);
                s.Add(r);
            }

            return s;
        }
    }
}