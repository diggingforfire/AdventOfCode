using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _04._01
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = File.ReadAllText("input.txt");

            int i = 1;

            while (true)
            {
                var bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(key + i++));
                // upper nibble (16^1 * F) should be 0 for hex string to start with 0
                if (bytes[0] == 0 && bytes[1] == 0 && (bytes[2] & 0xF0) == 0)
                    break;
            }

            Console.WriteLine(i);
            Console.ReadKey();
        }
    }
}