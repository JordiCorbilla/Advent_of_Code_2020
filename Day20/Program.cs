using System;
using System.Collections.Generic;
using System.IO;

namespace Day20
{
    public class Tile
    {
        public int Id { get; set; }
        public List<string> Content { get; set; }

        public Tile()
        {
            Content = new List<string>();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            var file = File.ReadAllLines("input.txt");
            var tiles = new List<Tile>();
            bool start = false;
            int id = -1;
            Tile tile = null;
            foreach (var row in file)
            {
                if (start && !string.IsNullOrEmpty(row))
                {
                    tile.Content.Add(row);
                }
                else if (string.IsNullOrEmpty(row))
                {
                    start = false;
                    tiles.Add(tile);
                }
                if (row.Contains("Tile"))
                {
                    tile = new Tile {Id = int.Parse(row.Replace("Tile ", "").Replace(":", ""))};
                    start = true;
                }
            }

            Console.WriteLine(tiles.Count);
        }
    }
}
