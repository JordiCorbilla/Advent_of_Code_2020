using System;
using System.Collections.Generic;
using System.IO;

namespace Day10
{
    class Program
    {
        static void Main()
        {
            var file = File.ReadAllLines("input.txt");
            List<int> sorted = new List<int>();

            foreach (var row in file)
            {
                sorted.Add(int.Parse(row));
            }
            sorted.Sort();

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
            Console.WriteLine($"Total: {difference3*difference1}");
        }
    }
}
