using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    public class Range
    {
        private readonly int _min;
        private readonly int _max;

        public Range(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public bool InRange(int value)
        {
            return value >= _min && value <= _max;
        }
    }
    class Program
    {
        static void Main(string[] args)
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
                    rangesTickets.Add(new Range(int.Parse(first[0].Trim()), int.Parse(first[1].Trim())));
                    rangesTickets.Add(new Range(int.Parse(second[0].Trim()), int.Parse(second[1].Trim())));
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
                    notInRange += ticket;
                }
            }
            Console.WriteLine(notInRange);
        }
    }
}
