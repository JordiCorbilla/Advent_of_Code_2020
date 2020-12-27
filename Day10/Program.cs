using System;
using System.Collections.Generic;
using System.IO;

namespace Day10
{
    class Program
    {
        public static int CountMax { get; set; }
        static void Main()
        {
            var file = File.ReadAllLines("inputtest.txt");
            List<int> sorted = new List<int>();

            foreach (var row in file)
            {
                sorted.Add(int.Parse(row));
            }
            sorted.Sort();

            Part1(sorted);
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
            int leaf = sorted[^1];

        }

        public static void GeneratePaths(List<int> sorted, int index, int previous)
        {
            for(int i=index; i<sorted.Count; i++)
            {
                if (previous + 1 == sorted[i])
                    GeneratePaths(sorted, i, sorted[i]);
                if (previous + 3 == sorted[i])
            }
        }
    }
}
