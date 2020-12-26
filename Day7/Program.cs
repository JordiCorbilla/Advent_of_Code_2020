﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Day7
{
    public class Node
    {
        public string Name { get; set; }
        public int Level { get; set; }
    }

    public class Bag
    {
        public int Capacity { get; set; }
        public string Name { get; set; }

        public Bag(int capacity, string name)
        {
            Capacity = capacity;
            Name = name;
        }
    }

    class Program
    {
        public static Dictionary<string, List<Bag>> Rules = new Dictionary<string, List<Bag>>();

        static void Main(string[] args)
        {
            var file = File.ReadAllLines("input.txt");
            
            foreach (var s in file)
            {
                var bags = s.Replace(".", "");
                if (!s.Contains("no other bags"))
                {
                    var sources = bags.Split("contain");
                    var key = sources[0];
                    var components = sources[1].Split(",");

                    key = key.Replace("bags", "").Replace("bag", "").Trim();
                    foreach (var component in components)
                    {
                        var number = int.Parse(component.Trim().Substring(0, component.Trim().IndexOf(" ", StringComparison.Ordinal)).Replace(" ", ""));
                        var nameBag = component.Replace(number.ToString(), "").Trim();
                        nameBag = nameBag.Replace("bags", "").Replace("bag", "").Trim();
                        BuildRule(key, nameBag, number);
                    }
                }
            }

            var p = JsonConvert.SerializeObject(Rules);
            Console.WriteLine(p);
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
    }
}
