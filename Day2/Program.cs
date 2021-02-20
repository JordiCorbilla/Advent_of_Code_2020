using System;
using System.IO;

namespace Day2
{
    internal class Program
    {
        private static void Main()
        {
            FirstInterpretation();
            SecondInterpretation();
        }

        private static void FirstInterpretation()
        {
            var data = File.ReadAllLines("Data.txt");
            //9-10 b: bbktbbbxhfbpb
            var countValid = 0;
            foreach (var line in data)
            {
                Console.WriteLine(line);
                var resource = line.Split(":");
                var text = resource[1].Trim();
                var characters = resource[0].Split(" ");
                var numbers = characters[0].Split("-");
                var character = characters[1];
                var min = int.Parse(numbers[0]);
                var max = int.Parse(numbers[1]);

                var occurrences = 0;
                foreach (var single in text)
                    if (single.ToString() == character)
                        occurrences++;

                var valid = occurrences >= min && occurrences <= max;

                if (valid)
                    countValid++;
            }

            Console.WriteLine($"{countValid}");
            Console.ReadKey();
        }

        private static void SecondInterpretation()
        {
            var data = File.ReadAllLines("Data.txt");
            //9-10 b: bbktbbbxhfbpb
            var countValid = 0;
            foreach (var line in data)
            {
                Console.WriteLine(line);
                var resource = line.Split(":");
                var text = resource[1].Trim();
                var characters = resource[0].Split(" ");
                var numbers = characters[0].Split("-");
                var character = characters[1];
                var min = int.Parse(numbers[0]);
                var max = int.Parse(numbers[1]);

                var valid = text[min - 1].ToString() == character;

                if (text[max - 1].ToString() == character) valid = !valid;

                if (valid)
                    countValid++;
            }

            Console.WriteLine($"{countValid}");
            Console.ReadKey();
        }
    }
}