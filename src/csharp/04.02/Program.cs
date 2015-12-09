using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _04._02
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = File.ReadAllText("input.txt");

            int i = 1;
            int result = 0;

            Parallel.For(i, Int32.MaxValue, new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount},
            (index, state) =>
            {
                int iLocal = System.Threading.Interlocked.Increment(ref i);

                var bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(key + iLocal));

                if (bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0)
                {
                    result = iLocal;
                    state.Stop();
                }
            });

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}