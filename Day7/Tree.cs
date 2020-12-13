using System.Collections.Generic;

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
}