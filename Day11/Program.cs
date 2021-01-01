using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    internal class Program
    {
        public static int MaxSeats { get; set; }
        public static int MaxRows { get; set; }

        private static void Main()
        {
            var seating = File.ReadAllLines("input.txt").ToList();

            var seat = -1;
            var row = 0;
            var backup = Clone(seating);

            MaxSeats = seating[0].Length-1;
            MaxRows = seating.Count-1;

            var previous1 = Clone(seating);
            while (SeatChanged(backup, previous1) || seat == -1)
            {
                List<string> merged = null;
                previous1 = Clone(backup);
                var previous = Clone(previous1);
                while (seat < MaxSeats || row < MaxRows)
                {
                    if (seat < MaxSeats)
                        seat++;
                    else
                    {
                        seat = 0;
                        if (row < MaxRows)
                        {
                            row++;
                        }
                    }
                    var changed = CheckDiagonal(seat, row, previous1);
                    merged = Merge(previous, changed, seat, row);
                    previous = Clone(merged);
                }

                backup = Clone(merged);
                row = 0;
                seat = 0;
            }
            var occupied = CountOccupiedSeats(backup);
            Console.WriteLine(occupied);
        }

        private static int CountOccupiedSeats(IEnumerable<string> backup)
        {
            return backup.SelectMany(seat => seat).Count(s => s == '#');
        }

        private static List<string> Merge(List<string> previous, IReadOnlyList<string> next, int seat, int row)
        {
            var merged = Clone(previous);
            var selectedRow = merged[row];
            var nextRow = next[row];

            var seats = selectedRow.ToCharArray();
            var nextSeats = nextRow.ToCharArray();
            seats[seat] = nextSeats[seat];
            var newArrangement = new string(seats);
            merged[row] = newArrangement;

            return merged;
        }

        private static List<string> CheckDiagonal(int seat, int row, List<string> seatingPlan)
        {
            var backup = Clone(seatingPlan);
            //Check the 8 adjacent sits
            var currentSeat = backup[row][seat];
            if (currentSeat == '.') return backup;
            var empty = 0;
            var max = 0;
            var occupied = 0;

            // XXX
            // XXO
            // XXX
            if (seat + 1 <= MaxSeats)
            {
                max++;
                var next = backup[row][seat + 1];
                switch (next)
                {
                    case 'L':
                    case '.':
                        empty++;
                        break;
                    case '#':
                        occupied++;
                        break;
                }
            }
            // XXX
            // XXX
            // XXO
            if (seat + 1 <= MaxSeats && row+1 <= MaxRows)
            {
                max++;
                var next = backup[row+1][seat + 1];
                switch (next)
                {
                    case 'L':
                    case '.':
                        empty++;
                        break;
                    case '#':
                        occupied++;
                        break;
                }
            }
            // XXX
            // XXX
            // XOX
            if (row + 1 <= MaxRows)
            {
                max++;
                var next = backup[row + 1][seat];
                switch (next)
                {
                    case 'L':
                    case '.':
                        empty++;
                        break;
                    case '#':
                        occupied++;
                        break;
                }
            }
            // XXX
            // XXX
            // OXX
            if (seat - 1 >= 0 && row + 1 <= MaxRows)
            {
                max++;
                var next = backup[row + 1][seat - 1];
                switch (next)
                {
                    case 'L':
                    case '.':
                        empty++;
                        break;
                    case '#':
                        occupied++;
                        break;
                }
            }
            // XXX
            // OXX
            // XXX
            if (seat - 1 >= 0)
            {
                max++;
                var next = backup[row][seat-1];
                switch (next)
                {
                    case 'L':
                    case '.':
                        empty++;
                        break;
                    case '#':
                        occupied++;
                        break;
                }
            }
            // OXX
            // XXX
            // XXX
            if (seat - 1 >= 0 && row - 1 >= 0)
            {
                max++;
                var next = backup[row - 1][seat - 1];
                switch (next)
                {
                    case 'L':
                    case '.':
                        empty++;
                        break;
                    case '#':
                        occupied++;
                        break;
                }
            }
            // XOX
            // XXX
            // XXX
            if (row - 1 >= 0)
            {
                max++;
                var next = backup[row - 1][seat];
                switch (next)
                {
                    case 'L':
                    case '.':
                        empty++;
                        break;
                    case '#':
                        occupied++;
                        break;
                }
            }
            // XXO
            // XXX
            // XXX
            if (seat + 1 <= MaxSeats && row - 1 >= 0)
            {
                max++;
                var next = backup[row - 1][seat + 1];
                switch (next)
                {
                    case 'L':
                    case '.':
                        empty++;
                        break;
                    case '#':
                        occupied++;
                        break;
                }
            }
            switch (currentSeat)
            {
                case 'L' when max == empty:
                {
                    var seats = backup[row];
                    var chars = seats.ToCharArray();
                    chars[seat] = '#';
                    var newArrangement = new string(chars);
                    backup[row] = newArrangement;
                    break;
                }
                case '#' when occupied >= 4:
                {
                    var seats = backup[row];
                    var chars = seats.ToCharArray();
                    chars[seat] = 'L';
                    var newArrangement = new string(chars);
                    backup[row] = newArrangement;
                    break;
                }
            }

            return backup;
        }

        public static List<string> Clone(List<string> list)
        {
            return list.ToList();
        }

        private static bool SeatChanged(IEnumerable<string> previousSeating, IReadOnlyList<string> newSeating)
        {
            var i = 0;
            return previousSeating.Any(t => t != newSeating[i++]);
        }
    }
}
