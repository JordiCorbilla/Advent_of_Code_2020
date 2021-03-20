using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using table.lib;

namespace Day16
{
    public class Range
    {
        private readonly int _max;
        private readonly int _max2;
        private readonly int _min;
        private readonly int _min2;
        private readonly string _name;

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
            return value >= _min && value <= _max || value >= _min2 && value <= _max2;
        }
    }

    public class RangeTable
    {
        public RangeTable(int column, string name)
        {
            Column = column;
            Name = name;
        }

        public int Column { get; }

        public string Name { get; }
    }

    public class KeyCount
    {
        public int Key { get; set; }
        public int Count { get; set; }
    }

    internal class Program
    {
        public static HashSet<int> Discarded { get; set; }
        public static List<string> NearbyTickets { get; set; }
        public static List<string> YourTickets { get; set; }
        public static List<Range> RangesTickets { get; set; }

        private static void Main(string[] args)
        {
            Discarded = new HashSet<int>();
            RangesTickets = new List<Range>();
            NearbyTickets = new List<string>();
            YourTickets = new List<string>();
            Part1();
            Console.WriteLine();
            Part2();
        }

        private static void Part2()
        {
            //Build classifier
            Console.WriteLine(Discarded.Count);
            //Clean up tickets
            var cleanTickets = new List<string>();
            var matrix = new List<IEnumerable<int>>();
            foreach (var row in NearbyTickets)
            {
                var numbers = row.Split(",").Select(int.Parse);
                var s = new List<int>();
                foreach (var number in numbers)
                    if (!Discarded.Contains(number))
                        s.Add(number);
                    else
                        s.Add(-1);

                matrix.Add(s);
            }

            Table<IEnumerable<int>>.Add(matrix, "T")
                .FilterColumns(new[] {"Capacity", "Count"})
                .ToConsole();

            //Check the first column to identify which are the numbers that belong to a range
            var rangeTables = new Dictionary<int, List<RangeTable>>();
            for (var i = 0; i < 20; i++)
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
                            rangeTables[i].Add(new RangeTable(i, range.GetName()));
                        else
                            rangeTables.Add(i, new List<RangeTable> {new RangeTable(i, range.GetName())});
                    }
                }

            Console.WriteLine("");

            var counts = rangeTables.ToDictionary(item => item.Key, item => item.Value.Count);

            var sorted = from items in counts
                orderby items.Value
                select items;

            foreach (var sort in sorted) Console.WriteLine($"{sort.Key}, {sort.Value}");

            Console.WriteLine("");
            var dictionarySorted = sorted.ToDictionary(x => x.Key, x => x.Value);
            //Check items one by one to determine order
            var taken = new Dictionary<string, int>();
            while (taken.Count != 20)
            {
                foreach (var index in dictionarySorted)
                {
                    var inspect = dictionarySorted[index.Key];
                    if (inspect == 1)
                    {
                        var category = rangeTables[index.Key][0].Name;
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

                counts = rangeTables.ToDictionary(item => item.Key, item => item.Value.Count);

                sorted = from items in counts
                    orderby items.Value
                    select items;

                dictionarySorted = sorted.ToDictionary(x => x.Key, x => x.Value);
            }

            Console.WriteLine("Classification:");
            Console.WriteLine(YourTickets[0]);
            var individual = YourTickets[0].Split(",").Select(int.Parse);
            var mytickets = individual
                .Select((s, index) => new {s, index})
                .ToDictionary(x => x.index, x => x.s);

            long amount = 1;
            foreach (var take in taken)
            {
                Console.WriteLine($"{take.Key}, {take.Value}, {mytickets[take.Value]}");
                if (take.Key.StartsWith("departure"))
                {
                    amount *= mytickets[take.Value];
                    Console.WriteLine(mytickets[take.Value]);
                }
            }

            Console.WriteLine(amount);
        }

        private static void Part1()
        {
            var file = File.ReadAllLines("input.txt");
            var startScanTickets = false;
            var startYourTickets = false;
            var tickets = new List<int>();

            foreach (var row in file)
            {
                if (startScanTickets)
                {
                    NearbyTickets.Add(row);
                    var ticketsScanned = row.Split(",");
                    tickets.AddRange(ticketsScanned.Select(int.Parse));
                }

                if (startYourTickets)
                {
                    if (row.Length > 10)
                        YourTickets.Add(row);
                    else
                        startYourTickets = false;
                }

                if (row.Contains("nearby tickets:"))
                {
                    startYourTickets = false;
                    startScanTickets = true;
                }
                else if (row.Contains("your ticket:"))
                {
                    startYourTickets = true;
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

            var notInRange = 0;
            foreach (var ticket in tickets)
            {
                var allInRange = false;
                foreach (var rangesTicket in RangesTickets)
                    if (rangesTicket.InRange(ticket))
                        allInRange = true;
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