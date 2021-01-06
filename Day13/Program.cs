using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    public class Bus {
        public int TimeStamp { get; set; }
        public bool Marked { get; set; }
        public int T { get; set; }
        public bool M { get; set; }

        public Bus(string pos, long index)
        {
            TimeStamp = int.Parse(pos);
            if (index % TimeStamp == 0)
                Marked = true;
        }

        public Bus(string pos, long index, int t)
        {
            TimeStamp = int.Parse(pos);
            if (index % TimeStamp == 0)
                Marked = true;
            T = t;
        }
    }
    class Program
    {
        static void Main()
        {
            //Part1();
            //Console.WriteLine();
            Part2();
        }

        private static void Part2()
        {
            var file = File.ReadAllLines("inputtest2.txt");
            var buses = file[1].Split(',');
            long pos = 0;
            long found = 0;
            var markList = new List<Bus>();
            for (int i = 1; i < buses.Length; i++)
            {
                if (buses[i] != "x")
                {
                    var b = new Bus(buses[i], 0, i);
                    markList.Add(b);
                }
            }

            //List<string> snapshot = new List<string> {"D..", "...", ".D.", "..D"};
            List<string> snapshot = new List<string> { "D....", ".D...", ".....", ".....", "..D..", ".....", "...D.", "D...D" };
            List<string> memory = new List<string>();
            while (true)
            {
                var busesList = new List<Bus>();
                string s = $"{pos} ->";
                string m = "";
                for (int i = 0; i < buses.Length; i++)
                {
                    if (buses[i] != "x")
                    {
                        var b = new Bus(buses[i], pos, i);
                        if (b.Marked)
                        {
                            s += "D";
                            m += "D";
                        }
                        else
                        {
                            s += ".";
                            m += ".";
                        }
                        busesList.Add(b);
                    }
                }
                //Console.WriteLine(s);
                memory.Add(m);

                //if (pos == 1068780)
                //{
                //    var f = "d";
                //    Console.WriteLine(f);
                //}

                if (busesList[0].Marked)
                {
                    found = pos;
                    if (Compare(memory, snapshot))
                    {
                        Console.WriteLine($"Found in {(found-memory.Count)+1}");
                        break;
                    }
                    memory.Clear();
                    memory.Add(m);
                }

                pos++;
            }
        }

        private static bool Compare(List<string> item, List<string> memory)
        {
            string one = "";
            foreach (var s in item)
            {
                one += s + "-";
            }

            string two = "";
            foreach (var s in memory)
            {
                two += s + "-";
            }

            return one.Contains(two);
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
            for (var i = timestamp - 50; i < timestamp; i++)
            {
                var s = "";
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
