using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    public class Range
    {
        private readonly string _name;
        private readonly int _min;
        private readonly int _max;
        private readonly int _min2;
        private readonly int _max2;

        public Range(string name, int min, int max, int min2, int max2)
        {
            _name = name;
            _min = min;
            _max = max;
            _min2 = min2;
            _max2 = max2;
        }

        public string GetName()
        {
            return _name;
        }

        public bool InRange(int value)
        {
            return (value >= _min && value <= _max) || (value >= _min2 && value <= _max2);
        }
    }
    class Program
    {
        public static HashSet<int> Discarded { get; set; }
        static void Main(string[] args)
        {
            Discarded = new HashSet<int>();
            Part1();
            Console.WriteLine();
            Part2();
        }

        private static void Part2()
        {
            //Build classifier
            Console.WriteLine(Discarded.Count);

        }

        private static void Part1()
        {
            var file = File.ReadAllLines("input.txt");
            bool startScanTickets = false;
            List<int> tickets = new List<int>();
            List<Range> rangesTickets = new List<Range>();
            foreach (var row in file)
            {
                if (startScanTickets)
                {
                    var ticketsScanned = row.Split(",");
                    tickets.AddRange(ticketsScanned.Select(int.Parse));
                }
                if (row.Contains("nearby tickets:"))
                {
                    startScanTickets = true;
                }
                //departure location: 44-709 or 728-964
                else if (row.Contains(": "))
                {
                    var ranges = row.Split(":");
                    var subRanges = ranges[1].Split("or");
                    var first = subRanges[0].Split("-");
                    var second = subRanges[1].Split("-");
                    rangesTickets.Add(new Range(
                            ranges[0].Trim(),
                        int.Parse(first[0].Trim()), 
                        int.Parse(first[1].Trim()),
                        int.Parse(second[0].Trim()),
                        int.Parse(second[1].Trim())));
                }
            }

            int notInRange = 0;
            foreach(var ticket in tickets)
            {
                bool allInRange = false;
                foreach (var rangesTicket in rangesTickets)
                {
                    if (rangesTicket.InRange(ticket))
                    {
                        allInRange = true;
                    }
                }
                if (!allInRange)
                {
                    Console.WriteLine(ticket);
                    Discarded.Add(ticket);
                    notInRange += ticket;
                }
            }
            Console.WriteLine(notInRange);
        }
    }
}
