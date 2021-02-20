using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day18
{
    internal class Program
    {
        public static Dictionary<string, long> Solutions = new Dictionary<string, long>();
        public static Dictionary<string, long> SubOperations = new Dictionary<string, long>();

        private static void Main()
        {
            //3 * (2 + (9 * 2 * 2 + 8) * (7 * 6 * 7 * 3) * (3 * 9 * 7) * (6 * 6)) * 9 * 8 + 6
            var result = ScanEquation("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2");
            Console.WriteLine(result);
            Solutions.Clear();
            result = ScanEquation2("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2");
            Console.WriteLine(result);
            Solutions.Clear();
            result = ScanEquation2("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))");
            Console.WriteLine(result);
            Solutions.Clear();
            result = ScanEquation2("1 + 2 * 3 + 4 * 5 + 6");
            Console.WriteLine(result);
            Solutions.Clear();
            result = ScanEquation2("2 + 8");
            Console.WriteLine(result);
            Solutions.Clear();
            result = ScanEquation2("6 * 5 * 3");
            Console.WriteLine(result);
            Solutions.Clear();
            result = ScanEquation2("2 * 3 + (4 * 5)");
            Console.WriteLine(result);
            Solutions.Clear();
            result = ScanEquation2("5 + (8 * 3 + 9 + 3 * 4 * 3)");
            Console.WriteLine(result);
            Solutions.Clear();
            result = ScanEquation2("1 + (2 * 3) + (4 * (5 + 6))");
            Console.WriteLine(result);

            Solutions.Clear();
            SubOperations.Clear();
            result = ScanEquation2(
                "9 + 3 + 5 + 3 * (5 + (8 + 7 * 8 * 8) * (5 + 9 + 9 * 9 * 4)) * (6 * (8 * 5 * 6 * 8 + 4 * 5))");
            Console.WriteLine(result);

            Solutions.Clear();
            Part1();
            Solutions.Clear();
            SubOperations.Clear();
            Part2();
        }

        public static void Part1()
        {
            var file = File.ReadAllLines("input.txt");
            var acc = file.Sum(ScanEquation);
            Console.WriteLine(acc);
        }

        public static void Part2()
        {
            var file = File.ReadAllLines("input.txt");
            long acc = 0;
            foreach (var row in file)
            {
                var sol = ScanEquation2(row);
                Console.WriteLine($"{row}={sol}");
                acc += sol;
            }

            Console.WriteLine(acc);
        }

        public static long ScanEquation(string s)
        {
            var resolve = "";
            var start = false;
            var processing = s;
            while (processing.Contains("("))
            {
                foreach (var digit in processing)
                {
                    if (start) resolve += digit;

                    switch (digit)
                    {
                        case '(' when !start:
                            start = true;
                            break;
                        case '(' when true:
                            resolve = "";
                            break;
                        case ')' when start:
                        {
                            start = false;
                            var equation = resolve.Replace(")", "");
                            var num = SolveEquation(equation);
                            if (!Solutions.ContainsKey(equation))
                                Solutions.Add(equation, num);
                            resolve = "";
                            break;
                        }
                    }
                }

                foreach (var (key, value) in Solutions)
                {
                    var replace = $"({key})";
                    processing = processing.Replace(replace, value.ToString());
                }

                Solutions.Clear();
            }

            return SolveEquation(processing);
        }

        public static long SolveEquation(string s)
        {
            //7 * 6 * 7 * 3
            var numbers = s.Split(" ");
            var accumulator = long.Parse(numbers[0]);
            var sum = false;
            var mul = false;
            for (var i = 1; i < numbers.Length; i++)
            {
                if (mul)
                {
                    accumulator *= long.Parse(numbers[i]);
                    mul = false;
                }

                if (sum)
                {
                    accumulator += long.Parse(numbers[i]);
                    sum = false;
                }

                switch (numbers[i])
                {
                    case "*":
                        mul = true;
                        break;
                    case "+":
                        sum = true;
                        break;
                }
            }

            return accumulator;
        }

        public static long ScanEquation2(string s)
        {
            var resolve = "";
            var start = false;
            var processing = s;
            while (processing.Contains("("))
            {
                foreach (var digit in processing)
                {
                    if (start) resolve += digit;

                    switch (digit)
                    {
                        case '(' when !start:
                            start = true;
                            break;
                        case '(' when true:
                            resolve = "";
                            break;
                        case ')' when start:
                        {
                            start = false;
                            var equation = resolve.Replace(")", "");
                            if (!Solutions.ContainsKey(equation))
                            {
                                var num = SolveEquationPlus(equation);
                                Solutions.Add(equation, num);
                            }

                            resolve = "";
                            break;
                        }
                    }
                }

                foreach (var (key, value) in Solutions)
                {
                    var replace = $"({key})";
                    processing = processing.Replace(replace, value.ToString());
                    if (!processing.Contains("("))
                        break;
                }

                Solutions.Clear();
            }

            return SolveEquationPlus(processing);
        }

        public static long SolveEquationPlus(string s)
        {
            //1 + 2 * 3 + 4 * 5 + 6
            //1 * 2 + 3
            var resolve = s;

            while (resolve.Contains("+"))
            {
                var numbers = resolve.Split(" ");
                long left = -1;
                long right = -1;
                var plusFound = false;

                foreach (var t in numbers)
                {
                    if (plusFound && right == -1 && t != "+" && t != "*")
                    {
                        right = long.Parse(t);

                        if (!SubOperations.ContainsKey($"{left} + {right}"))
                        {
                            var calc = left + right;
                            SubOperations.Add($"{left} + {right}", calc);
                        }
                    }

                    if (left == -1 && t != "+" && t != "*")
                        left = long.Parse(t);
                    switch (t)
                    {
                        case "+":
                            plusFound = true;
                            break;
                        case "*":
                            left = -1;
                            break;
                    }
                }

                foreach (var (key, value) in SubOperations)
                {
                    var replace = $"{key}";
                    resolve = resolve.Replace(replace, value.ToString());
                    if (!resolve.Contains("+"))
                        break;
                }

                SubOperations.Clear();
            }

            return SolveEquation(resolve);
        }
    }
}