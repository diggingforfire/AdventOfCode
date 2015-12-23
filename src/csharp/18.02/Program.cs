using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace _18._02
{
    class Program
    {
        private static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var grid = new bool[lines.Length, lines.Length];

            // Initialize grid
            for (int x = 0; x < lines.Length; x++)
                for (int y = 0; y < lines.Length; y++)
                    grid[x, y] = lines[x][y] == '#';

            // Turn on corners
            grid[0,0] = grid[0, lines.Length -1] = grid[lines.Length -1, 0] = grid[lines.Length -1,lines.Length -1] = true;

            const int stepCount = 100;
          
            // Perform steps
            for (int s = 0; s < stepCount; s++)
            {
                //PrintGrid(grid, lines.Length);

                var points = new List<Point>();

                for (int x = 0; x < lines.Length; x++)
                {
                    for (int y = 0; y < lines.Length; y++)
                    {
                        var neighboursOnCount = CountNeighboursOn(x, y, grid, lines.Length);
                        if ( IsCorner(x, y, lines.Length) || (neighboursOnCount == 3 || (neighboursOnCount == 2 && grid[x,y] )))
                            points.Add(new Point(x, y));
                    }
                }

                for (int x = 0; x < lines.Length; x++)
                    for (int y = 0; y < lines.Length; y++)
                        grid[x, y] = points.Any(p => p.X == x && p.Y == y);
            }

            Console.WriteLine(grid.Cast<bool>().Sum(b => b ? 1 : 0));
            Console.ReadKey();
        }

        private static bool IsCorner(int x, int y, int length)
        {
            return (x == 0 && y == 0) || (x == 0 && y == length -1) || (y == 0 && x == length - 1) || (y == length - 1 && x == length - 1);
        }

        private static int CountNeighboursOn(int x, int y, bool[,] grid, int length)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    if (i != 0 || j != 0)
                        if (x + i >= 0 && y + j >= 0 && x + i <= length - 1 && y + j <= length - 1)
                            if (grid[x + i, y + j])
                                count++;
            return count;
        }

        private static void PrintGrid(bool[,] grid, int length)
        {
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    Console.Write(grid[x,y] ? '#' : '.');
                }

                Console.Write(Environment.NewLine);
            }

            Console.Write(Environment.NewLine);
            Console.Write(Environment.NewLine);
        }
    }
}
