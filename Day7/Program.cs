using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = File.ReadAllLines("input.txt");
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
                        if (newRules.Count > 0)
                            rules.AddRange(newRules);
                    }
                }
            }

            HashSet<string> unique = new HashSet<string>();

            foreach (var rule in rules)
            {
                var json = JsonConvert.SerializeObject(rule);
                Console.WriteLine(json);
                Console.WriteLine();

                var nodesVisited = new List<string>();
                TraverseTree(rule, "shiny gold", nodesVisited);

                var memory = new List<string>();
                foreach (var node in nodesVisited)
                {
                    memory.Add(node);
                    if (node != "shiny gold") continue;
                    var itemsToAdd = memory.Where(x => x != "shiny gold");
                    foreach (var found in itemsToAdd)
                    {
                        unique.Add(found);
                    }
                    memory = new List<string>();
                }
            }
            
            Console.WriteLine(unique.Count);
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

        static void TraverseTree(Tree node, string name, List<string> nodesVisited)
        {
            nodesVisited.Add(node.Description);
            if (node.Description == name)
            {
                return;
            }
            foreach (var nodeNode in node.Nodes)
            {
                TraverseTree(nodeNode, name, nodesVisited);
            }
        }
    }
}
