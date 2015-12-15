using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._02
{
    class Program
    {
        static readonly char[] forbidden = { 'i', 'o', 'l' };

        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder(File.ReadAllText("input.txt"));

            while (!Match(sb = Increment(sb))) { }
            while (!Match(sb = Increment(sb))) { }

            Console.WriteLine(sb.ToString());
            Console.ReadKey();
        }

        static bool Match(StringBuilder s)
        {
            string p = s.ToString();
            return CheckForPairs(p) && CheckForIncreasingStraight(p) && !forbidden.Any(p.Contains);
        }

        static bool CheckForPairs(string s)
        {
            var res = s.Skip(1).Select((c, i) => new { a = s[i], b = c, Index = i }).Where(pair => pair.a == pair.b).GroupBy(pair => $"{pair.a}{pair.b}").ToList();
            return res.Count >= 2;
        }

        static bool CheckForIncreasingStraight(string s)
        {
            return s.Skip(2).Select((c, i) => new { a = s[i], b = s[++i], c, Index = i }).Any(p => p.a + 1 == p.b && p.b + 1 == p.c);
        }

        static StringBuilder Increment(StringBuilder sb)
        {
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                sb[i] = sb[i] == 'z' ? 'a' : ++sb[i];
                if (sb[i] != 'a')
                    break;
            }

            int forbiddenIndex = sb.ToString().IndexOf('i');
            if (forbiddenIndex > -1)
            {
                sb[forbiddenIndex]++;
                for (int i = ++forbiddenIndex; i < sb.Length; i++)
                    sb[i] = 'a';
            }

            return sb;
        }
    }
}
