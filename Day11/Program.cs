using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        public static int MaxSeats { get; set; }
        public static int MaxRows { get; set; }

        static void Main()
        {
            var seating = File.ReadAllLines("inputtest.txt").ToList();

            int seat = -1;
            int row = 0;
            var backup = Clone(seating);

            MaxSeats = seating[0].Length-1;
            MaxRows = seating.Count-1;

            var previous1 = Clone(seating);
            while (!SeatChanged(backup, previous1))
            {
                List<string> merged = null;
                previous1 = Clone(backup);
                var previous = Clone(seating);
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
                    var changed = CheckDiagonal(seat, row, seating);
                    merged = Merge(previous, changed, seat, row);
                    previous = Clone(merged);
                }

                backup = Clone(merged);
                row = 0;
            }
        }

        private static List<string> Merge(List<string> previous, List<string> next, int seat, int row)
        {
            var merged = Clone(previous);
            var selectedRow = merged[row];
            var nextRow = next[row];

            var seats = selectedRow.ToCharArray();
            var nextSeats = nextRow.ToCharArray();
            seats[seat] = nextSeats[seat];
            var newArrangement = new string(seats);
            merged[row] = newArrangement;

            //for (int i = 0; i < merged.Count; i++)
            //{
            //    if (merged[i] != next[i])
            //    {
            //        var seats = merged[i].ToCharArray();
            //        var nextSeats = next[i].ToCharArray();
            //        for (int j = 0; j < seats.Length; j++)
            //        {
            //            seats[j] = nextSeats[j];
            //        }
            //        var newArrangement = new string(seats);
            //        merged[i] = newArrangement;
            //    }
            //}

            return merged;
        }

        private static List<string> CheckDiagonal(int seat, int row, List<string> seatingPlan)
        {
            var backup = Clone(seatingPlan);
            //Check the 8 adjacent sits
            var currentSeat = backup[row][seat];
            if (currentSeat != '.')
            {
                int empty = 0;
                int max = 0;
                int occupied = 0;

                if (seat + 1 < MaxSeats)
                {
                    max++;
                    var next = backup[row][seat + 1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (seat + 1 < MaxSeats && row+1 < MaxRows)
                {
                    max++;
                    var next = backup[row+1][seat + 1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (row + 1 < MaxRows)
                {
                    max++;
                    var next = backup[row + 1][seat];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (seat - 1 > 0 && row + 1 < MaxRows)
                {
                    max++;
                    var next = backup[row + 1][seat - 1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (seat - 1 > 0)
                {
                    max++;
                    var next = backup[row][seat-1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (seat - 1 > 0 && row - 1 > 0)
                {
                    max++;
                    var next = backup[row - 1][seat - 1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (row - 1 > 0)
                {
                    max++;
                    var next = backup[row - 1][seat];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (seat + 1 < MaxSeats && row - 1 > 0)
                {
                    max++;
                    var next = backup[row - 1][seat + 1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (currentSeat == 'L' && max == empty)
                {
                    var seats = backup[row];
                    char[] chars = seats.ToCharArray();
                    chars[seat] = '#';
                    var newArrangement = new string(chars);
                    backup[row] = newArrangement;
                }
                else if (currentSeat == '#' && occupied >= 4)
                {
                    var seats = backup[row];
                    char[] chars = seats.ToCharArray();
                    chars[seat] = 'L';
                    var newArrangement = new string(chars);
                    backup[row] = newArrangement;
                }
            }

            return backup;
        }

        public static List<string> Clone(List<string> list)
        {
            var backup = new List<string>();
            foreach (var item in list)
                backup.Add(item);
            return backup;
        }

        private static bool SeatChanged(IEnumerable<string> previousSeating, IReadOnlyList<string> newSeating)
        {
            int i = 0;
            foreach (var t in previousSeating)
            {
                if (t != newSeating[i++]) return true;
            }

            return false;
        }


    }
}
