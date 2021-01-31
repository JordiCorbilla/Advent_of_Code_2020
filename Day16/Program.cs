using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using table.lib;

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

    public class RangeTable{
        private readonly int _column;
        private readonly string _name;

        public RangeTable(int column, string name)
        {
            _column = column;
            _name = name;
        }

        public int Column => _column;
        public string Name => _name;

    }

    public class KeyCount
    {
        public int Key { get; set; }
        public int Count { get; set; }
    }

    class Program
    {
        public static HashSet<int> Discarded { get; set; }
        public static List<string> NearbyTickets { get; set; }
        public static List<Range> RangesTickets { get; set; }
        static void Main(string[] args)
        {
            Discarded = new HashSet<int>();
            RangesTickets = new List<Range>();
            NearbyTickets = new List<string>();
            Part1();
            Console.WriteLine();
            Part2();
        }

        private static void Part2()
        {
            //Build classifier
            Console.WriteLine(Discarded.Count);
            //Clean up tickets
            List<string> cleanTickets = new List<string>();
            var matrix = new List<IEnumerable<int>>();
            foreach (var row in NearbyTickets)
            {
                var numbers = row.Split(",").Select(int.Parse);
                var s = new List<int>();
                foreach (var number in numbers)
                {
                    if (!Discarded.Contains(number))
                        s.Add(number);
                    else
                    {
                        s.Add(-1);
                    }
                }

                matrix.Add(s);
            }

            Table<IEnumerable<int>>.Add(matrix,"T")
                .FilterOutColumns(new[] { "Capacity", "Count" })
                .ToConsole();

            //Check the first column to identify which are the numbers that belong to a range
            var rangeTables = new Dictionary<int, List<RangeTable>>();
            for (int i = 0; i < 20; i++)
            {
                foreach (var range in RangesTickets)
                {
                    var belongs = true;
                    foreach (var row in matrix)
                    {
                        var s = row.ToList();

                        if (!range.InRange(s[i]) && s[i] != -1)
                        {
                            belongs = false;
                            break;
                        }
                    }

                    if (belongs)
                    {
                        Console.WriteLine($"Column:{i}, {range.GetName()}: Belongs: {belongs}");
                        if (rangeTables.ContainsKey(i))
                        {
                            rangeTables[i].Add(new RangeTable(i, range.GetName()));
                        }
                        else
                        {
                            rangeTables.Add(i, new List<RangeTable>{new RangeTable(i, range.GetName())});
                        }
                    }
                }
            }

            Console.WriteLine("");

            var counts = rangeTables.
                ToDictionary(item => item.Key, item => item.Value.Count);

            var sorted = from items in counts
                orderby items.Value ascending
                select items;

            foreach (var sort in sorted)
            {
                Console.WriteLine($"{sort.Key}, {sort.Value}");
            }

            Console.WriteLine("");
            var dictionarySorted = sorted.ToDictionary(x => x.Key, x => x.Value);
            //Check items one by one to determine order
            Dictionary<string, int> taken = new Dictionary<string, int>();
            while (taken.Count != 20)
            {
                foreach (var index in dictionarySorted)
                {
                    var inspect = dictionarySorted[index.Key];
                    if (inspect == 1)
                    {
                        string category = rangeTables[index.Key][0].Name;
                        taken.Add(category, index.Key);
                        
                        foreach (var h in rangeTables)
                        {
                            var itemToRemove = h.Value.SingleOrDefault(r => r.Name == category);
                            if (itemToRemove != null)
                                h.Value.Remove(itemToRemove);
                        }
                        rangeTables.Remove(index.Key);
                        break;
                    }
                }

                counts = rangeTables.
                    ToDictionary(item => item.Key, item => item.Value.Count);

                sorted = from items in counts
                    orderby items.Value ascending
                    select items;

                dictionarySorted = sorted.ToDictionary(x => x.Key, x => x.Value);
            }

            Console.WriteLine("Classification:");
            foreach (var take in taken)
            {
                Console.WriteLine($"{take.Key}, {take.Value}");
            }
        }

        private static void Part1()
        {
            var file = File.ReadAllLines("input.txt");
            bool startScanTickets = false;
            List<int> tickets = new List<int>();
            
            foreach (var row in file)
            {
                if (startScanTickets)
                {
                    NearbyTickets.Add(row);
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
                    RangesTickets.Add(new Range(
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
                foreach (var rangesTicket in RangesTickets)
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
