using System;
using System.IO;
using Microsoft.VisualBasic;

namespace Day5
{
    class Program
    {
        // 0 - 127
        // 0 - 63 | 64 - 127
        // 0 - 32 | 32 - 63 | 64 - 95 | 96 - 127
        // 
        static void Main(string[] args)
        {
            var file = File.ReadAllLines("input.txt");

            int highest = 0;
            foreach (var s in file)
            {
                var rows = s.Substring(0, 7);
                var columns = s.Substring(7, 3);
                Console.WriteLine($"{rows}, {columns}");
                int min;
                int minMax;
                int maxMax;
                int max;
                (min, minMax, maxMax, max) = GenerateRange(0, 127);
                foreach (var i in rows)
                {
                    if (i == 'F')
                    {
                        (min, minMax, maxMax, max) = GenerateRange(min, minMax);
                    }
                    else if (i == 'B')
                    {
                        (min, minMax, maxMax, max) = GenerateRange(maxMax, max);
                    }
                }

                var row = min;
                (min, minMax, maxMax, max) = GenerateRange(0, 7);
                foreach (var i in columns)
                {
                    if (i == 'L')
                    {
                        (min, minMax, maxMax, max) = GenerateRange(min, minMax);
                    }
                    else if (i == 'R')
                    {
                        (min, minMax, maxMax, max) = GenerateRange(maxMax, max);
                    }
                }

                var column = min;
                var seatId = (row * 8) + column;
                if (seatId > highest)
                    highest = seatId;
                Console.WriteLine(seatId);
            }
            Console.WriteLine(highest);
        }

        static (int, int, int, int) GenerateRange(int min, int max)
        {
            var maxMinRange = ((max-min) / 2) + min;
            var minMaxRange = maxMinRange + 1;

            return (min, maxMinRange, minMaxRange, max);
        }
    }
}
