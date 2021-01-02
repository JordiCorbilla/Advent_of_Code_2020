using System;
using System.IO;
using System.Linq;

namespace Day12
{
    public enum Direction
    {
        East,
        West,
        North,
        South
    }
    class Program
    {
        static void Main()
        {
            Part1();
        }

        public static void Part1()
        {
            var directions = File.ReadAllLines("input.txt").ToList();
            var moving = Direction.East;
            var east = 0;
            var north = 0;
            foreach (var direction in directions)
            {

                var step = int.Parse(direction.Substring(1));
                switch(direction[0])
                {
                    case 'N':
                        north += step;
                        break;
                    case 'S':
                        north -= step;
                        break;
                    case 'E':
                        east += step;
                        break;
                    case 'W':
                        east -= step;
                        break;
                    case 'L':
                        moving = step switch
                        {
                            90 => Direction.North,
                            180 => Direction.West,
                            270 => Direction.South,
                            _ => moving
                        };
                        break;
                    case 'R':
                        moving = step switch
                        {
                            90 => Direction.South,
                            180 => Direction.West,
                            270 => Direction.North,
                            _ => moving
                        };
                        break;
                    case 'F':
                        switch (moving)
                        {
                            case Direction.East:
                                east += step;
                                break;
                            case Direction.West:
                                east -= step;
                                break;
                            case Direction.North:
                                north += step;
                                break;
                            case Direction.South:
                                north -= step;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                }
            }
            Console.WriteLine($"East: {east}, North: {north}, Manhattan Distance: {Math.Abs(east)+Math.Abs(north)}");
        }
    }
}
