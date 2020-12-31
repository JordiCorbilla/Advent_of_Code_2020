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

            int seat = 0;
            int row = 0;
            var backup = new List<string>();
            foreach(var item in seating)
                backup.Add(item);

            MaxSeats = seating[seat].Length;
            MaxRows = seating.Count;

            while (!SeatChanged(backup, seating))
            {
                while (seat < MaxSeats && row < MaxRows)
                {
                    CheckDiagonal(seat, row, seating);

                    if (seat < MaxSeats)
                        seat++;
                    else
                    {
                        seat = 0;
                    }

                    if (row < MaxRows)
                    {
                        row++;
                    }
                }
                backup = new List<string>();
                foreach (var item in seating)
                    backup.Add(item);
                row = 0;
            }
        }

        private static void CheckDiagonal(int seat, int row, List<string> seatingPlan)
        {
            //Check the 8 adjacent sits
            var currentSeat = seatingPlan[row][seat];
            if (currentSeat != '.')
            {
                int empty = 0;
                int max = 0;
                int occupied = 0;

                if (seat + 1 < MaxSeats)
                {
                    max++;
                    var next = seatingPlan[row][seat + 1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (seat + 1 < MaxSeats && row+1 < MaxRows)
                {
                    max++;
                    var next = seatingPlan[row+1][seat + 1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (row + 1 < MaxRows)
                {
                    max++;
                    var next = seatingPlan[row + 1][seat];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (seat - 1 > 0 && row + 1 < MaxRows)
                {
                    max++;
                    var next = seatingPlan[row + 1][seat - 1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (seat - 1 > 0)
                {
                    max++;
                    var next = seatingPlan[row][seat-1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (seat - 1 > 0 && row - 1 > 0)
                {
                    max++;
                    var next = seatingPlan[row - 1][seat - 1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (row - 1 > 0)
                {
                    max++;
                    var next = seatingPlan[row - 1][seat];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }
                if (seat + 1 < MaxSeats && row - 1 > 0)
                {
                    max++;
                    var next = seatingPlan[row - 1][seat + 1];
                    if (next == 'L' || next == '.')
                        empty++;
                    if (next == '#')
                        occupied++;
                }

                if (currentSeat == 'L' && max == empty)
                {
                    var seats = seatingPlan[row];
                    char[] chars = seats.ToCharArray();
                    chars[seat] = '#';
                    var newArrangement = new string(chars);
                    seatingPlan[row] = newArrangement;
                }
                else if (currentSeat == '#' && occupied >= 4)
                {
                    var seats = seatingPlan[row];
                    char[] chars = seats.ToCharArray();
                    chars[seat] = 'L';
                    var newArrangement = new string(chars);
                    seatingPlan[row] = newArrangement;
                }
            }
        }


        private static bool SeatChanged(IEnumerable<string> previousSeating, IReadOnlyList<string> newSeating)
        {
            return previousSeating.Where((t, i) => t != newSeating[i]).Any();
        }


    }
}
