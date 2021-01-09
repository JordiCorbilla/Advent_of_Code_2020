using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    class Program
    {
        static void Main()
        {
            //Part1();
            //Console.WriteLine();
            Part2();
        }

        private static void Part2()
        {
            var file = File.ReadAllLines("input.txt");

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

                    var decompose = FloatingDecoderCombinations(currentMask);

                    long acc = 0;
                    foreach (var mask in decompose)
                    {
                        acc += BitMaskV2(mask, ToBase36(value));
                    }

                    if (memory.ContainsKey(pos))
                    {
                        memory[pos] = acc;
                    }
                    else
                    {
                        memory.Add(pos, acc);
                    }
                }
            }

            long acc2 = 0;
            foreach (var item in memory)
            {
                acc2 += item.Value;
            }
            Console.WriteLine(acc2);
        }

        private static void Part1()
        {
            var file = File.ReadAllLines("input.txt");
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
            int exponent = 35;
            double result = 0;
            foreach (var digit in base36)
            {
                if (digit == '1')
                    result += Math.Pow(2, exponent);
                exponent--;
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

        private static long BitMaskV2(string mask, string base36)
        {
            var result = mask.ToCharArray();
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '1')
                    result[i] = base36[i];
            }

            return FromBase36(new string(result));
        }

        private static List<string> FloatingDecoderCombinations(string mask)
        {
            var sequence = new List<string> {mask};

            while (true)
            {
                var generated = new List<string>();
                foreach (var m in sequence)
                {
                    var chars = m.ToCharArray();

                    for (var i = 0; i < m.Length; i++)
                    {
                        if (m[i] == 'X')
                        {
                            chars[i] = '0';
                            generated.Add(new string(chars));
                            chars[i] = '1';
                            generated.Add(new string(chars));
                            break;
                        }
                    }

                }
                sequence.Clear();
                sequence.AddRange(generated);

                if (!ContainsX(sequence))
                    break;
            }

            return sequence;
        }

        private static bool ContainsX(List<string> list)
        {
            return list.Any(x => x.Contains("X"));
        }
    }
}
