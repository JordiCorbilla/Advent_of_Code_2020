using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Day18
{
    class Program
    {
        public static Dictionary<string, int> Solutions = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            //3 * (2 + (9 * 2 * 2 + 8) * (7 * 6 * 7 * 3) * (3 * 9 * 7) * (6 * 6)) * 9 * 8 + 6
            ScanEquation("3 * (2 + (9 * 2 * 2 + 8) * (7 * 6 * 7 * 3) * (3 * 9 * 7) * (6 * 6)) * 9 * 8 + 6");

        }

        public static int ScanEquation(string s)
        {
            var resolve = "";
            bool start = false;
            string processing = s;
            while (processing.Contains("("))
            {
                foreach (var digit in processing)
                {
                    if (start)
                    {
                        resolve += digit;
                    }

                    if (digit == '(' && !start)
                    {
                        start = true;
                    }
                    else if (digit == '(' && start)
                    {
                        resolve = "";
                    }
                    else if (digit == ')')
                    {
                        start = false;
                        var equation = resolve.Replace(")", "");
                        int num = SolveEquation(equation);
                        Solutions.Add(equation, num);
                    }
                }

                foreach (var item in Solutions)
                {
                    string replace = $"({item.Key})";
                    processing = processing.Replace(replace, item.Value.ToString());
                }
            }

            return SolveEquation(processing);
        }

        public static int SolveEquation(string s)
        {
            //7 * 6 * 7 * 3
            var numbers = s.Split(" ");
            var accumulator =int.Parse(numbers[0]);
            bool sum = false;
            bool mul = false;
            for (int i = 1; i < numbers.Length; i++)
            {
                if (mul)
                {
                    accumulator *= int.Parse(numbers[i]);
                    mul = false;
                }

                if (sum)
                {
                    accumulator += int.Parse(numbers[i]);
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

    }
}
