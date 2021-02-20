using System;
using System.Collections.Generic;
using System.IO;

namespace Day20
{
    public class Tile
    {
        public Tile()
        {
            Content = new List<string>();
        }

        public int Id { get; set; }
        public List<string> Content { get; set; }
    }

    public class Matrix
    {
        public Matrix()
        {
            Cells = new char[10][];
            Position = 0;
        }

        public Matrix(char[][] cells)
        {
            Cells = cells;
            Position = 0;
        }

        public char[][] Cells { get; set; }
        public int Position { get; set; }

        public void Add(string line)
        {
            var s = new char[10];
            for (var i = 0; i < 10; i++) s[i] = line[i];
            Cells[Position++] = s;
        }

        public void Print()
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++) Console.Write($"{Cells[i][j]}");
                Console.Write("\n");
            }
        }
    }

    public class Generator
    {
        private readonly Matrix _matrix;

        public Generator(Matrix matrix)
        {
            _matrix = matrix;
            Count = 5;
        }

        public int Count { get; set; }

        public Matrix Generate(int index)
        {
            switch (index)
            {
                case 0:
                default:
                    return _matrix;
                case 1:
                    var h = FlipHorizontal(_matrix.Cells);
                    return new Matrix(h);
                case 2:
                    var h2 = FlipHorizontal(_matrix.Cells);
                    var v = FlipVertical(h2);
                    return new Matrix(v);
                case 3:
                    var v2 = FlipVertical(_matrix.Cells);
                    var h3 = FlipHorizontal(v2);
                    return new Matrix(h3);
                case 4:
                    var v4 = FlipVertical(_matrix.Cells);
                    return new Matrix(v4);
            }
        }

        public char[][] FlipHorizontal(char[][] item)
        {
            var s = new char[10][];
            for (var i = 0; i < 10; i++) s[i] = new char[10];

            for (var i = 0; i < 10; i++)
            for (var j = 0; j < 10; j++)
                s[9 - i][j] = item[i][j];

            return s;
        }

        public char[][] FlipVertical(char[][] item)
        {
            var s = new char[10][];
            for (var i = 0; i < 10; i++) s[i] = new char[10];

            for (var i = 0; i < 10; i++)
            for (var j = 0; j < 10; j++)
                s[i][9 - j] = item[i][j];

            return s;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = new string[10][];

            var file = File.ReadAllLines("input.txt");
            var tiles = new List<Matrix>();
            var start = false;
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

                    var generator = new Generator(tile);
                    for (var i = 0; i < generator.Count; i++)
                    {
                        var r = generator.Generate(i);
                        r.Print();
                        Console.WriteLine();
                    }
                }

                if (row.Contains("Tile"))
                {
                    tile = new Matrix(); // { Id = int.Parse(row.Replace("Tile ", "").Replace(":", "")) };
                    start = true;
                }
            }

            //Console.WriteLine(tiles.Count);
            //string[][] a = new string[720][];
        }
    }
}