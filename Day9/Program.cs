using System;
using System.Collections.Generic;
using System.IO;

namespace Day9
{
    internal class Program
    {
        private static void Main()
        {
            var file = File.ReadAllLines("input.txt");
            var preambleSize = 25;
            long solution = 0;

            var explorer = new long[preambleSize];

            for (var i = 0; i < preambleSize; i++)
                explorer[i] = long.Parse(file[i]);

            for (var index = preambleSize; index < file.Length; index++)
            {
                var row = file[index];
                var belongs = CheckIfBelongs(row, explorer);
                if (!belongs)
                {
                    Console.WriteLine($"{row} does not belong!");
                    solution = long.Parse(row);
                    break;
                }

                PushNewValue(explorer, row);
            }

            Console.WriteLine(solution);
            Part2(solution, file);
        }

        private static void PushNewValue(long[] explorer, string row)
        {
            for (var i = 1; i < explorer.Length; i++) explorer[i - 1] = explorer[i];

            explorer[^1] = long.Parse(row);
        }

        public static bool CheckIfBelongs(string row, long[] queue)
        {
            //if A = B + C -> A-B = C
            var value = long.Parse(row);
            var operation = new Dictionary<long, bool>();
            foreach (var l in queue)
                if (!operation.ContainsKey(l))
                    operation.Add(l, true);

            foreach (var l in queue)
                if (operation.ContainsKey(value - l))
                    return true;

            return false;
        }

        public static void Part2(long solution, string[] file)
        {
            var index = 0;
            var solutionBackup = solution;
            var found = false;
            var minValue = long.MaxValue;
            var maxValue = long.MinValue;
            while (!found)
            {
                for (var i = index; i < file.Length; i++)
                {
                    solution -= long.Parse(file[i]);

                    if (long.Parse(file[i]) < minValue)
                        minValue = long.Parse(file[i]);
                    if (long.Parse(file[i]) > maxValue)
                        maxValue = long.Parse(file[i]);

                    if (solution == 0)
                    {
                        //found
                        Console.WriteLine($"Min[{i}]: {minValue}, Max[{i}]: {maxValue}, Sum: {minValue + maxValue}");
                        found = true;
                        break;
                    }

                    if (solution < 0)
                    {
                        solution = solutionBackup;
                        minValue = long.MaxValue;
                        maxValue = long.MinValue;
                        break;
                    }
                }

                index++;
            }
        }
    }
}