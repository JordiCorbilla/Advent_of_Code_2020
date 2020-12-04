using System;
using System.IO;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var trees = SlideThrough();
            Console.WriteLine(trees);
            long a = CheckProbability(1,1);
            long b = CheckProbability(5, 1);
            long c = CheckProbability(7, 1);
            long d = CheckProbability(1, 2);
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.WriteLine(d);
            long result = a * b * c * d * trees;
            Console.WriteLine(result);
        }

        public static int SlideThrough()
        {
            var file = File.ReadAllLines("input.txt");
            var posX = 0;
            var posY = 0;

            var maxY = file.Length - 1;
            int countTrees = 0;
            while (posY <= maxY)
            {
                var line = file[posY];
                var character = line[posX];
                Console.WriteLine(character);
                if (character.ToString() == "#")
                    countTrees++;
                posY++;
                posX = posX + 3;
                if (posX > (line.Length - 1))
                {
                    Console.WriteLine("Slide");
                    posX = posX - (line.Length);
                }
            }
            return countTrees;
        }

        public static int CheckProbability(int stepsX, int stepsY)
        {
            var file = File.ReadAllLines("input.txt");
            var posX = 0;
            var posY = 0;

            var maxY = file.Length - 1;
            int countTrees = 0;
            while (posY <= maxY)
            {
                var line = file[posY];
                var character = line[posX];
                //Console.WriteLine(character);
                if (character.ToString() == "#")
                    countTrees++;
                posY = posY + stepsY;
                posX = posX + stepsX;
                if (posX > (line.Length - 1))
                {
                    //Console.WriteLine("Slide");
                    posX = posX - (line.Length);
                }
            }

            return countTrees;
        }
    }
}
