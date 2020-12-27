using System;
using System.Collections.Generic;
using System.IO;

namespace Day9
{
    class Program
    {
        static void Main()
        {
            var file = File.ReadAllLines("input.txt");
            int preambleSize = 25;

            var explorer = new long[preambleSize];

            for (int i = 0; i < preambleSize; i++)
                explorer[i] = long.Parse(file[i]);

            for (var index = preambleSize; index < file.Length; index++)
            {
                var row = file[index];
                var belongs = CheckIfBelongs(row, explorer);
                if (!belongs)
                {
                    Console.WriteLine($"{row} does not belong!");
                    break;
                }

                PushNewValue(explorer, row);
            }
        }

        private static void PushNewValue(long[] explorer, string row)
        {
            for (int i = 1; i < explorer.Length; i++)
            {
                explorer[i - 1] = explorer[i];
            }

            explorer[^1] = long.Parse(row);
        }

        public static bool CheckIfBelongs(string row, long[] queue)
        {
            //if A = B + C -> A-B = C
            long value = long.Parse(row);
            Dictionary<long, bool> operation = new Dictionary<long, bool>();
            foreach (var l in queue)
            {
                if (!operation.ContainsKey(l))
                    operation.Add(l, true);
            }

            foreach (var l in queue)
            {
                if (operation.ContainsKey(value - l))
                    return true;
            }

            return false;
        }
    }
}
