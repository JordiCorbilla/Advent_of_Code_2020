using System;
using System.Collections.Generic;
using System.IO;

namespace Day10
{
    class Program
    {
        public static long CountMax { get; set; }
        static void Main()
        {
            var file = File.ReadAllLines("input.txt");
            List<int> sorted = new List<int>();

            foreach (var row in file)
            {
                sorted.Add(int.Parse(row));
            }
            sorted.Sort();

            Part1(sorted);
            CountMax = 0;
            Part2(sorted);
        }

        public static void Part1(List<int> sorted)
        {
            int jolted = 0;
            int difference1 = 0;
            int difference2 = 0;
            int difference3 = 0;
            foreach (var item in sorted)
            {
                if (jolted + 1 == item)
                {
                    difference1++;
                }
                else if (jolted + 2 == item)
                {
                    difference2++;
                }
                else if (jolted + 3 == item)
                {
                    difference3++;
                }
                else
                {
                    Console.WriteLine("can't continue");
                    break;
                }

                jolted = item;
            }

            jolted += 3;
            difference3++;
            Console.WriteLine(jolted);
            Console.WriteLine($"difference1: {difference1}");
            Console.WriteLine($"difference2: {difference2}");
            Console.WriteLine($"difference3: {difference3}");
            Console.WriteLine($"Total: {difference3 * difference1}");
        }

        public static void Part2(List<int> sorted)
        {
            Dictionary<int, int> items = new Dictionary<int, int>();
            foreach(var item in sorted)
                items.Add(item, item);
            GeneratePaths(items, 0, sorted[^1]);
            Console.WriteLine($"Max leaves: {CountMax}");
        }

        public static void GeneratePaths(Dictionary<int, int> sorted, int previous, int leaf)
        {
            if (sorted.ContainsKey(previous + 1))
                GeneratePaths(sorted, sorted[previous + 1], leaf);
            if (sorted.ContainsKey(previous + 2))
                GeneratePaths(sorted, sorted[previous + 2], leaf);
            if (sorted.ContainsKey(previous + 3))
                GeneratePaths(sorted, sorted[previous + 3], leaf);
            if (previous==leaf)
                CountMax++;
        }
    }
}
