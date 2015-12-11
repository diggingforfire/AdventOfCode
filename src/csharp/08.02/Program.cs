using System;
using System.IO;
using System.Linq;

namespace _08._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            Console.WriteLine(lines.Sum(l => l.Replace("\\", "\\\\").Replace("\"", "\\\"").Length + 2 - l.Length));
            Console.ReadKey();
        }
    }
}