using System;
using System.Collections.Generic;
using System.IO;

namespace Day7
{
    public class Bag
    {
        public Bag(int capacity, string name)
        {
            Capacity = capacity;
            Name = name;
        }

        public int Capacity { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    internal class Program
    {
        public static Dictionary<string, List<Bag>> Rules = new Dictionary<string, List<Bag>>();
        public static Dictionary<string, List<Bag>> RulesTree = new Dictionary<string, List<Bag>>();
        public static HashSet<string> NodesVisited = new HashSet<string>();

        private static void Main()
        {
            var file = File.ReadAllLines("input.txt");

            foreach (var s in file)
            {
                var bags = s.Replace(".", "");
                if (s.Contains("no other bags")) continue;
                var sources = bags.Split("contain");
                var key = sources[0];
                var components = sources[1].Split(",");

                key = key.Replace("bags", "").Replace("bag", "").Trim();
                foreach (var component in components)
                {
                    var number = int.Parse(component.Trim()
                        .Substring(0, component.Trim().IndexOf(" ", StringComparison.Ordinal)).Replace(" ", ""));
                    var nameBag = component.Replace(number.ToString(), "").Trim();
                    nameBag = nameBag.Replace("bags", "").Replace("bag", "").Trim();
                    BuildRule(key, nameBag, number);
                }
            }

            Console.WriteLine();
            var shiny = Rules["shiny gold"];
            TraverseUp(shiny);
            foreach (var bag in shiny)
            {
                Console.WriteLine(bag);
            }

            Console.WriteLine(NodesVisited.Count);

            Part2();
        }

        private static void BuildRule(string container, string name, int capacity)
        {
            if (!Rules.ContainsKey(name))
            {
                var keys = new List<Bag> {new Bag(capacity, container)};
                Rules.Add(name, keys);
            }
            else
            {
                var keys = Rules[name];
                keys.Add(new Bag(capacity, container));
            }
        }

        private static void TraverseUp(IEnumerable<Bag> bags)
        {
            foreach (var bag in bags)
            {
                NodesVisited.Add(bag.Name);
                if (!Rules.ContainsKey(bag.Name)) continue;
                var container = Rules[bag.Name];
                TraverseUp(container);
            }
        }

        private static void Part2()
        {
            var file = File.ReadAllLines("input.txt");

            foreach (var s in file)
            {
                var bags = s.Replace(".", "");
                if (s.Contains("no other bags")) continue;
                var sources = bags.Split("contain");
                var key = sources[0];
                var components = sources[1].Split(",");

                key = key.Replace("bags", "").Replace("bag", "").Trim();
                var list = new List<Bag>();
                foreach (var component in components)
                {
                    var number = int.Parse(component.Trim()
                        .Substring(0, component.Trim().IndexOf(" ", StringComparison.Ordinal)).Replace(" ", ""));
                    var nameBag = component.Replace(number.ToString(), "").Trim();
                    nameBag = nameBag.Replace("bags", "").Replace("bag", "").Trim();
                    list.Add(new Bag(number, nameBag));
                }

                RulesTree.Add(key, list);
            }

            var startRoot = RulesTree["shiny gold"];
            int count = TraverseDown(startRoot);
            Console.WriteLine($"Capacity: {count}");
        }

        private static int TraverseDown(IEnumerable<Bag> bags)
        {
            var num = 0;

            foreach (var bag in bags)
            {
                num += (bag.Capacity);
                if (!RulesTree.ContainsKey(bag.Name)) continue;
                var inside = TraverseDown(RulesTree[bag.Name]);
                num += inside * bag.Capacity;
            }

            return num;
        }
    }
}