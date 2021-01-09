using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Console.WriteLine();
            Part2();
        }

        private static void Part2()
        {
            
        }

        private static void Part1()
        {
            var file = File.ReadAllLines("inputtest.txt");
            var currentMask = "";
            Dictionary<long, long> memory = new Dictionary<long, long>();
            foreach (var row in file)
            {
                if (row.Contains("mask ="))
                {
                    currentMask = row.Replace("mask = ", "");
                }
                //mem[8] = 11
                if (row.Contains("mem["))
                {
                    var mem = row.Replace("mem[", "").Replace("] = ", ",").Split(",");
                    long pos = long.Parse(mem[0]);
                    long value = long.Parse(mem[1]);
                    var result = BitMask(currentMask, ToBase36(value));
                    if (memory.ContainsKey(pos))
                    {
                        memory[pos] = result;
                    }
                    else
                    {
                        memory.Add(pos, result);
                    }
                }
            }

            long acc = 0;
            foreach (var item in memory)
            {
                acc += item.Value;
            }
            Console.WriteLine(acc);
        }

        private static string ToBase36(long number)
        {
            var result = "";
            var num = number;
            while (num > 0)
            {
                var rem = num % 2;
                num = num / 2;
                result = rem + result;
            }

            return $"{new string('0', 36 - result.Length)}{result}";
        }

        private static long FromBase36(string base36)
        {
            double exponent = 36;
            double result = 0;
            foreach (var digit in base36)
            {
                result += Math.Pow((double)2, exponent--);
            }

            return (long)result;
        }

        private static long BitMask(string mask, string base36)
        {
            var result = mask.ToCharArray();
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                    result[i] = base36[i];
            }

            return FromBase36(new string(result));
        }
    }
}
