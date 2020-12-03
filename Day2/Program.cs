using System;
using System.IO;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstInterpretation();
            SecondInterpretation();
        }

        static void FirstInterpretation()
        {
            var data = File.ReadAllLines("Data.txt");
            //9-10 b: bbktbbbxhfbpb
            int countValid = 0;
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

                int occurrences = 0;
                foreach (var single in text)
                {
                    if (single.ToString() == character)
                    {
                        occurrences++;
                    }
                }

                bool valid = occurrences >= min && occurrences <= max;

                if (valid)
                    countValid++;
            }
            Console.WriteLine($"{countValid}");
            Console.ReadKey();
        }

        static void SecondInterpretation()
        {
            var data = File.ReadAllLines("Data.txt");
            //9-10 b: bbktbbbxhfbpb
            int countValid = 0;
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

                bool valid = text[min - 1].ToString() == character;

                if (text[max - 1].ToString() == character)
                {
                    valid = !valid;
                }
                
                if (valid)
                    countValid++;
            }
            Console.WriteLine($"{countValid}");
            Console.ReadKey();
        }
    }
}
