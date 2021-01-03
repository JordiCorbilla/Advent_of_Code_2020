using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    public class Bus {
        public int TimeStamp { get; set; }
        public bool Marked { get; set; }

        public Bus(string pos, long index)
        {
            TimeStamp = int.Parse(pos);
            if (index % TimeStamp == 0)
                Marked = true;
        }
    }
    class Program
    {
        static void Main()
        {
            Part1();
            Console.WriteLine();
            Part2();
        }

        private static void Part2()
        {
            
        }

        private static void Part1()
        {
            var file = File.ReadAllLines("input.txt");
            var timestamp = long.Parse(file[0]);
            var initialTimestamp = timestamp;
            var buses = file[1].Split(',').Where(x => x != "x");
            var schedule = new List<List<Bus>>();
            var enumerable = buses as string[] ?? buses.ToArray();
            timestamp += 50;
            for (long i = 0; i < timestamp; i++)
            {
                var busesList = new List<Bus>();
                foreach (var t in enumerable)
                {
                    var bus = new Bus(t, i);
                    busesList.Add(bus);
                }
                schedule.Add(busesList);
            }

            var trueFound = 0;
            var busLine = 0;
            for (long i = timestamp - 50; i < timestamp; i++)
            {
                string s = "";
                foreach (var p in schedule[(int) i])
                {
                    s += $"{p.TimeStamp}({p.Marked}), ";
                    if (p.Marked && trueFound == 0)
                    {
                        trueFound = (int)i;
                        busLine = p.TimeStamp;
                    }
                }
                Console.WriteLine($"{i}, {s}");
            }

            Console.WriteLine($"Solution = {(trueFound - initialTimestamp)*busLine}");
        }
    }
}
