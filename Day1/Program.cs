using System;
using System.Collections.Generic;

namespace Day1
{
    internal class Program
    {
        private static void Main()
        {
            var data = DataInput.Get();
            var calculation = new Dictionary<int, int>();
            foreach (var number in data)
            {
                var result = 2020 - number;
                if (!calculation.ContainsKey(result))
                    calculation.Add(result, number);
            }

            foreach (var number in data)
                if (calculation.ContainsKey(number))
                {
                    Console.WriteLine($"{number}, {calculation[number]}");
                    Console.WriteLine($"{number * calculation[number]}");
                    break;
                }

            for (var i = 0; i < data.Length; i++)
            for (var j = i + 1; j < data.Length; j++)
                if (calculation.ContainsKey(data[i] + data[j]))
                {
                    Console.WriteLine($"{data[i]}, {data[j]}, {calculation[data[i] + data[j]]}");
                    Console.WriteLine($"{data[i] * data[j] * calculation[data[i] + data[j]]}");
                    Console.ReadKey();
                    return;
                }
        }
    }
}