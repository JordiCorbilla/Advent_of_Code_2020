using System;
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
                                    var subtree = new Tree(number, nameBag);
                                    var subNodes = SearchNode(rule, subtree.Description);
                                    if (subNodes != null)
                                    {
                                        foreach (var j in subNodes.Nodes)
                                        {
                                            subtree.Nodes.Add(j);
                                        }
                                    }
                                    root.Nodes.Add(subtree);
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
                if (json.Contains("shiny gold"))
                {
                    Console.WriteLine(json);
                    Console.WriteLine();

                    var nodesVisited = new List<Node>();
                    var level = 0;
                    TraverseTree(rule, "shiny gold", nodesVisited, level);

                    foreach (var bags in nodesVisited)
                    {
                        Console.WriteLine($"Level: {bags.Level}:{bags.Name}");
                    }

                    Console.WriteLine();

                    var memory = new Dictionary<int, string>();

                    int levelMultiplier = 1;
                    if (!memory.ContainsKey(nodesVisited[0].Level))
                        memory.Add(nodesVisited[0].Level, nodesVisited[0].Name);

                    for (var index = 1; index < nodesVisited.Count; index++)
                    {
                        if (nodesVisited[index].Level < nodesVisited[index - 1].Level)
                            levelMultiplier += 100;

                        var node = nodesVisited[index];
                        if (!memory.ContainsKey(node.Level * levelMultiplier))
                            memory.Add(node.Level * levelMultiplier, node.Name);
                        else
                        {
                            if (memory[node.Level * levelMultiplier] != "shiny gold")
                                memory[node.Level * levelMultiplier] = node.Name;
                        }
                    }

                    foreach (var mem in memory)
                    {
                        if (mem.Value != "shiny gold")
                            unique.Add(mem.Value);
                    }




                    //foreach (var node in nodesVisited)
                    //{
                    //    currentLevel = node.Level;
                    //    memory.Add(node.Name);
                    //    if (node.Name != "shiny gold")
                    //    {

                    //    }
                    //    var itemsToAdd = memory.Where(x => x != "shiny gold");
                    //    foreach (var found in itemsToAdd)
                    //    {
                    //        unique.Add(found);
                    //    }
                    //    memory = new List<string>();
                    //}
                }
            }

            foreach (var bags in unique)
            {
                Console.WriteLine(bags);
            }
            Console.WriteLine();
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

        static void TraverseTree(Tree node, string name, List<Node> nodesVisited, int level)
        {
            nodesVisited.Add(new Node { Name = node.Description, Level = level });
            if (node.Description == name)
            {
                return;
            }
            foreach (var nodeNode in node.Nodes)
            {
                TraverseTree(nodeNode, name, nodesVisited, level + 1);
            }
        }
    }
}
