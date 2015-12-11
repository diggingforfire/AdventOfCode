using System;
using System.IO;
using System.Linq;

namespace _01._02
{
    class Program
    {
        static void Main(string[] args)
        {
            const char up = '(';
            string input = File.ReadAllText("input.txt");
       
            int counter = 0;
                
            int index = input.TakeWhile(c => (counter += c == up ? 1 : -1) >= 0).Count() + 1;

            Console.WriteLine(index);
            Console.ReadKey();
        }
    }
}