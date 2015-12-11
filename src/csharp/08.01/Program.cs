using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _08._01
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            Console.WriteLine(lines.Sum(l => l.Length - Regex.Unescape(l).Length - 2));
            Console.ReadKey();
        }
    }
}