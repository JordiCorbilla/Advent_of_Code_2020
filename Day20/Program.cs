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

    public class Matrix
    {
        public char[][] Cells { get; set; }
        public int Position { get; set; }
        public Matrix()
        {
            Cells = new char[10][];
            Position = 0;
        }

        public void Add(string line)
        {
            var s = new char[10];
            for (int i = 0; i < 10; i++)
            {
                s[i] = line[i];
            }
            Cells[Position++] = s;
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            string[][] a = new string[10][];



            //var file = File.ReadAllLines("input.txt");
            //var tiles = new List<Tile>();
            //bool start = false;
            //Tile tile = null;
            //foreach (var row in file)
            //{
            //    if (start && !string.IsNullOrEmpty(row))
            //    {
            //        tile.Content.Add(row);
            //    }
            //    else if (string.IsNullOrEmpty(row))
            //    {
            //        start = false;
            //        tiles.Add(tile);
            //    }
            //    if (row.Contains("Tile"))
            //    {
            //        tile = new Tile {Id = int.Parse(row.Replace("Tile ", "").Replace(":", ""))};
            //        start = true;
            //    }
            //}

            //Console.WriteLine(tiles.Count);
            //string[][] a = new string[720][];

        }
    }
}
