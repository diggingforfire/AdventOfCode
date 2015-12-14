using System;
using System.IO;
using System.Linq;

namespace _10._01
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText("input.txt");

            for (int p = 0, id = 0; p < 40; p++)
                text = string.Join("", text.Select((c, i) => new { c, Id = i == text.Length - 1 ? id : c == text[++i] ? id : id++ }).GroupBy(g => g).Select(g => g.Count() + g.Key.c.ToString()));
            
            Console.WriteLine(text.Length);
            Console.ReadKey();
        }
    }
}