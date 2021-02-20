using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void Print()
        {
            for(int i=0; i<10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write($"{Cells[i][j]}");
                }
                Console.Write("\n");
            }
        }

        public void Print(char[][] s)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write($"{s[i][j]}");
                }
                Console.Write("\n");
            }
        }

        public char[][] Rotate()
        {
            var s = new char[10][];
            for (int i = 0; i < 10; i++)
            {
                s[i] = new char[10];
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    s[9-i][j] = Cells[i][j];
                }
            }

            return s;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            string[][] a = new string[10][];

            var file = File.ReadAllLines("input.txt");
            var tiles = new List<Matrix>();
            bool start = false;
            Matrix tile = null;
            foreach (var row in file)
            {
                if (start && !string.IsNullOrEmpty(row))
                {
                    tile.Add(row);
                }
                else if (string.IsNullOrEmpty(row))
                {
                    start = false;
                    tiles.Add(tile);
                    tile.Print();
                    Console.WriteLine();
                    var s = tile.Rotate();
                    tile.Print(s);
                }
                if (row.Contains("Tile"))
                {
                    tile = new Matrix();// { Id = int.Parse(row.Replace("Tile ", "").Replace(":", "")) };
                    start = true;
                }
            }

            //Console.WriteLine(tiles.Count);
            //string[][] a = new string[720][];

        }
    }
}
