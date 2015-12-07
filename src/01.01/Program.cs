using System;
using System.IO;
using System.Linq;

namespace _01._01
{
    class Program
    {
        static void Main(string[] args)
        {
            const char up = '(';
            string input = File.ReadAllText("input.txt");
            int resultFloor = input.Sum(c => c == up ? 1  : -1);
            Console.Write(resultFloor);
        }
    }
}
