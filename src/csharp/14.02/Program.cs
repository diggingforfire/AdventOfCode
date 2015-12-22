using System;
using System.IO;
using System.Linq;

namespace _14._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var reindeer = File.ReadAllLines("input.txt")
              .Select(line => line.Split(' '))
              .Select(parts => new { Name = parts[0], Speed = int.Parse(parts[3]), FlyDuration = int.Parse(parts[6]), RestDuration = int.Parse(parts[13]) })
              .Select(r => new { r.Name, r.Speed, r.FlyDuration, r.RestDuration, FlyRestDuration = r.FlyDuration + r.RestDuration, DistancePerPeriod = r.Speed * r.FlyDuration })
              .ToList();

            const int seconds = 2503;
            var max = reindeer.Max(r =>
            {
                // sum points for every second, point is awarded if reindeer is in the group of leaders for that second
                return Enumerable.Range(1, seconds).Sum(i =>
                    reindeer.GroupBy(rr => GetDistanceAtSecond(i, rr.FlyDuration, rr.RestDuration, rr.DistancePerPeriod, rr.Speed)).OrderByDescending(g => g.Key).FirstOrDefault().Contains(r) ? 1 : 0
                );
            });
            
            Console.WriteLine(max);
            Console.ReadKey();
        }

        static int GetDistanceAtSecond(int second, int flyDuration, int restDuration, int distancePerPeriod, int speed)
        {
            double numPeriods = second / (double)(flyDuration + restDuration);
            int distance = (int)Math.Floor(numPeriods) * distancePerPeriod;
            int remS = second % (flyDuration + restDuration);
            if (remS > flyDuration)
                remS = flyDuration;

            distance += (remS*speed);

            return distance;
        }
    }
}