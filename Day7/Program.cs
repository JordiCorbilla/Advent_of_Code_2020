using System;
using System.Collections.Generic;
using System.IO;

namespace Day7
{
    public class Tree
    {
        public List<Tree> Nodes { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }

        public Tree(int count, string description)
        {
            Count = count;
            Description = description;
            Nodes = new List<Tree>();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var file = File.ReadAllLines("inputtest.txt");
            var rules = new List<Tree>();

            foreach (var s in file)
            {
                var bags = s.Replace(".", "");
                //dark salmon bags contain 2 faded teal bags, 4 drab white bags, 3 posh bronze bags.
                //drab maroon bags contain no other bags.
                if (!s.Contains("no other bags"))
                {
                    var sources = bags.Split("contain");
                    var node = sources[0];
                    var rest = sources[1].Split(",");

                    if (rules.Count == 0)
                    {
                        node = node.Replace("bags", "").Replace("bag", "").Trim();
                        var root = new Tree(1, node);
                        foreach (var bag in rest)
                        {
                            int number = int.Parse(bag.Trim().Substring(0, bag.Trim().IndexOf(" ", StringComparison.Ordinal)).Replace(" ", ""));
                            var nameBag = bag.Replace(number.ToString(), "").Trim();
                            nameBag = nameBag.Replace("bags", "").Replace("bag", "").Trim();
                            root.Nodes.Add(new Tree(number, nameBag));
                        }
                        rules.Add(root);
                    }
                    else
                    {
                        //do a search
                        var newRules = new List<Tree>();
                        bool alreadyAdded = false;
                        foreach (var rule in rules)
                        {
                            node = node.Replace("bags", "").Replace("bag", "").Trim();
                            var nodeTree = SearchNode(rule, node);

                            if (nodeTree == null && !alreadyAdded)
                            {
                                node = node.Replace("bags", "").Replace("bag", "").Trim();
                                var root = new Tree(1, node);
                                foreach (var bag in rest)
                                {
                                    int number = int.Parse(bag.Trim().Substring(0, bag.Trim().IndexOf(" ", StringComparison.Ordinal)).Replace(" ", ""));
                                    var nameBag = bag.Replace(number.ToString(), "").Trim();
                                    nameBag = nameBag.Replace("bags", "").Replace("bag", "").Trim();
                                    root.Nodes.Add(new Tree(number, nameBag));
                                }
                                newRules.Add(root);
                                break;
                            }

                            if(nodeTree != null)
                            {
                                foreach (var bag in rest)
                                {
                                    int number = int.Parse(bag.Trim().Substring(0, bag.Trim().IndexOf(" ", StringComparison.Ordinal)).Replace(" ", ""));
                                    var nameBag = bag.Replace(number.ToString(), "").Trim();
                                    nameBag = nameBag.Replace("bags", "").Replace("bag", "").Trim();
                                    nodeTree.Nodes.Add(new Tree(number, nameBag));
                                }

                                alreadyAdded = true;
                            }
                        }
                        rules.AddRange(newRules);
                    }
                }
            }

            foreach (var rule in rules)
            {
                Console.WriteLine(rule);
            }
        }

        static Tree SearchNode(Tree node, string name)
        {
            if (node.Description == name)
            {
                return node;
            }

            foreach (var nodeNode in node.Nodes)
            {
                var nodeFound = SearchNode(nodeNode, name);
                if (nodeFound != null)
                    return nodeFound;
            }

            return null;
        }
    }
}
