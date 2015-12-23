using System;
using System.IO;
using System.Linq;

namespace _14._01
{
    class Program
    {
        static void Main(string[] args)
        {
            var reindeer = File.ReadAllLines("input.txt")
                .Select(line => line.Split(' '))
                .Select(parts => new {Name = parts[0], Speed = int.Parse(parts[3]), FlyDuration = int.Parse(parts[6]), RestDuration = int.Parse(parts[13])})
                .Select(r => new { r.Name, r.Speed, r.FlyDuration, r.RestDuration, FlyRestDuration = r.FlyDuration + r.RestDuration, DistancePerPeriod = r.Speed * r.FlyDuration })
                .ToList();

            const int seconds = 2503;
            int maxDistance = 0;

            foreach (var r in reindeer)
            {
                double numPeriods = seconds / (double)r.FlyRestDuration;
                int totalDistance = (int)Math.Floor(numPeriods)*r.DistancePerPeriod;
                double remainder = seconds%r.FlyRestDuration;

                if (remainder > r.FlyDuration)
                    totalDistance += r.DistancePerPeriod;

                if (totalDistance > maxDistance)
                    maxDistance = totalDistance;
            }

            Console.WriteLine(maxDistance);
            Console.ReadKey();
        }
    }
}