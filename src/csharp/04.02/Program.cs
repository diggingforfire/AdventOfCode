﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace _04._02
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = File.ReadAllText("input.txt");

            int maxThreads = Environment.ProcessorCount;
            var semaphore = new SemaphoreSlim(maxThreads, maxThreads);

            bool found = false;

            var results = new List<int>();

            int i = 0;

            while (true)
            {
                if (found)
                {
                    if (maxThreads == semaphore.CurrentCount)
                        break;
              
                    continue;
                }

                semaphore.Wait();
        
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    try
                    {
                        int iLocal = System.Threading.Interlocked.Increment(ref i);

                        var bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(key + iLocal));

                        if (bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0)
                        {
                            results.Add(iLocal);
                            found = true;
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });
            }

            Console.WriteLine(results.Min());
            Console.ReadKey();
        }
    }
}