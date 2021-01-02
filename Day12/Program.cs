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
            var south = 0;
            var west = 0;
            foreach (var direction in directions)
            {
                var step = int.Parse(direction.Substring(1));
                switch(direction[0])
                {
                    case 'N':
                        north += step;
                        break;
                    case 'S':
                        south += step;
                        break;
                    case 'E':
                        east += step;
                        break;
                    case 'W':
                        west += step;
                        break;
                    case 'L':
                        switch (moving)
                        {
                            case Direction.East:
                                moving = step switch
                                {
                                    90 => Direction.North,
                                    180 => Direction.West,
                                    270 => Direction.South,
                                    _ => moving
                                };
                                break;
                            case Direction.West:
                                moving = step switch
                                {
                                    90 => Direction.South,
                                    180 => Direction.East,
                                    270 => Direction.North,
                                    _ => moving
                                };
                                break;
                            case Direction.North:
                                moving = step switch
                                {
                                    90 => Direction.West,
                                    180 => Direction.South,
                                    270 => Direction.East,
                                    _ => moving
                                };
                                break;
                            case Direction.South:
                                moving = step switch
                                {
                                    90 => Direction.East,
                                    180 => Direction.North,
                                    270 => Direction.West,
                                    _ => moving
                                };
                                break;
                        }
                        break;
                    case 'R':
                        switch (moving)
                        {
                            case Direction.East:
                                moving = step switch
                                {
                                    90 => Direction.South,
                                    180 => Direction.West,
                                    270 => Direction.North,
                                    _ => moving
                                };
                                break;
                            case Direction.West:
                                moving = step switch
                                {
                                    90 => Direction.North,
                                    180 => Direction.East,
                                    270 => Direction.South,
                                    _ => moving
                                };
                                break;
                            case Direction.North:
                                moving = step switch
                                {
                                    90 => Direction.East,
                                    180 => Direction.South,
                                    270 => Direction.West,
                                    _ => moving
                                };
                                break;
                            case Direction.South:
                                moving = step switch
                                {
                                    90 => Direction.West,
                                    180 => Direction.North,
                                    270 => Direction.East,
                                    _ => moving
                                };
                                break;
                        }
                        break;
                    case 'F':
                        switch (moving)
                        {
                            case Direction.East:
                                east += step;
                                break;
                            case Direction.West:
                                west += step;
                                break;
                            case Direction.North:
                                north += step;
                                break;
                            case Direction.South:
                                south += step;
                                break;
                        }
                        break;
                }
            }
            Console.WriteLine($"East: {east}, North: {north}");
            Console.WriteLine($"West: {west}, South: {south}");
            Console.WriteLine($"Manhattan Distance: {Math.Abs(east- west) + Math.Abs(north- south)}");
        }
    }
}
