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
            Console.WriteLine();
            Part2();
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
        public static void Part2()
        {
            var directions = File.ReadAllLines("input.txt").ToList();
            var wayPointEast = 10.0;
            var wayPointNorth = 1.0;
            var shipEast = 0.0;
            var shipNorth = 0.0;
            foreach (var direction in directions)
            {
                var step = double.Parse(direction.Substring(1));
                switch (direction[0])
                {
                    case 'N':
                        wayPointNorth += step;
                        break;
                    case 'S':
                        wayPointNorth -= step;
                        break;
                    case 'E':
                        wayPointEast += step;
                        break;
                    case 'W':
                        wayPointEast -= step;
                        break;
                    case 'L':
                        // This is the case for a 2D rotation
                        // x' = x cos(a) - y sin(a)
                        // y' = x sin(a) - y cos(a)
                        // where a is the angle to rotate anti-clockwise
                        var rads = Math.PI / 180 * step;
                        var prevEast = wayPointEast;
                        var prevNorth = wayPointNorth;
                        wayPointEast = (prevEast * Math.Cos(rads)) - (prevNorth * Math.Sin(rads));
                        wayPointNorth = (prevEast * Math.Sin(rads)) + (prevNorth * Math.Cos(rads));
                        break;
                    case 'R':
                        // This is the case for a 2D rotation
                        // x' = x cos(-a) - y sin(-a)
                        // y' = x sin(-a) - y cos(-a)
                        // where a is the angle to rotate clockwise
                        step *= -1;
                        double radians = Math.PI / 180 * step;
                        var previousEast = wayPointEast;
                        var previousNorth = wayPointNorth;
                        wayPointEast = (previousEast * Math.Cos(radians)) - (previousNorth * Math.Sin(radians));
                        wayPointNorth = (previousEast * Math.Sin(radians)) + (previousNorth * Math.Cos(radians));
                        break;
                    case 'F':
                        shipEast += step * wayPointEast;
                        shipNorth += step * wayPointNorth;
                        break;
                }
            }
            Console.WriteLine($"East: {shipEast}, North: {shipNorth}");
            Console.WriteLine($"Manhattan Distance: {Math.Abs(shipEast) + Math.Abs(shipNorth)}");
        }
    }
}
