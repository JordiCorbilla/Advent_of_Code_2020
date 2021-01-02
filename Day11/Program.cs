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
            Part1();
            Part2();
        }

        private static void Part1()
        {
            var seating = File.ReadAllLines("input.txt").ToList();

            var seat = -1;
            var row = 0;
            var backup = Clone(seating);

            MaxSeats = seating[0].Length - 1;
            MaxRows = seating.Count - 1;

            var previous1 = Clone(seating);
            while (SeatKeepsChanging(backup, previous1) || seat == -1)
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

                    var changed = CheckMostAdjacent(seat, row, previous1);
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

        private static void Part2()
        {
            var seating = File.ReadAllLines("input.txt").ToList();

            var seat = -1;
            var row = 0;
            var backup = Clone(seating);

            MaxSeats = seating[0].Length - 1;
            MaxRows = seating.Count - 1;

            var previous1 = Clone(seating);
            while (SeatKeepsChanging(backup, previous1) || seat == -1)
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

                    var changed = CheckDeepAdjacent(seat, row, previous1);
                    merged = Merge(previous, changed, seat, row);
                    previous = Clone(merged);
                }

                backup = Clone(merged);
                Print(backup);
                row = 0;
                seat = 0;
            }

            var occupied = CountOccupiedSeats(backup);
            Console.WriteLine(occupied);
        }

        private static void Print(IEnumerable<string> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
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

        private static List<string> CheckMostAdjacent(int seat, int row, List<string> seatingPlan)
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

        private static List<string> CheckDeepAdjacent(int seat, int row, List<string> seatingPlan)
        {
            var backup = Clone(seatingPlan);
            //Check the 8 adjacent sits
            var currentSeat = backup[row][seat];
            if (currentSeat == '.') return backup;
            var empty = 0;
            var visited = 0;
            var occupied = 0;

            // XXX
            // XXO
            // XXX
            if (seat + 1 <= MaxSeats)
            {
                var cent = seat;
                var next = backup[row][++cent];
                visited++;

                switch (next)
                {
                    case 'L':
                        empty++;
                        break;
                    case '.':
                        while (next == '.' && cent < MaxSeats)
                        {
                            next = backup[row][++cent];
                        }

                        if (next == '.')
                            next = 'L';
                        switch (next)
                        {
                            case 'L': 
                                empty++;
                                break;
                            case '#': 
                                occupied++;
                                break;
                        }
                        break;
                    case '#':
                        occupied++;
                        break;
                }
            }
            // XXX
            // XXX
            // XXO
            if (seat + 1 <= MaxSeats && row + 1 <= MaxRows)
            {
                var cent = seat;
                var cent2 = row;
                var next = backup[++cent2][++cent];
                visited++;

                switch (next)
                {
                    case 'L':
                        empty++;
                        break;
                    case '.':
                        while (next == '.' && cent < MaxSeats && cent2 < MaxRows)
                        {
                            next = backup[++cent2][++cent];
                        }

                        if (next == '.')
                            next = 'L';
                        switch (next)
                        {
                            case 'L':
                                empty++;
                                break;
                            case '#':
                                occupied++;
                                break;
                        }
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
                var cent = row;
                var next = backup[++cent][seat];
                visited++;

                switch (next)
                {
                    case 'L':
                        empty++;
                        break;
                    case '.':
                        while (next == '.' && cent < MaxRows)
                        {
                            next = backup[++cent][seat];
                        }

                        if (next == '.')
                            next = 'L';
                        switch (next)
                        {
                            case 'L':
                                empty++;
                                break;
                            case '#':
                                occupied++;
                                break;
                        }
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
                var cent = seat;
                var cent2 = row;
                var next = backup[++cent2][--cent];
                visited++;

                switch (next)
                {
                    case 'L':
                        empty++;
                        break;
                    case '.':
                        while (next == '.' && cent > 0 && cent2 < MaxRows)
                        {
                            next = backup[++cent2][--cent];
                        }

                        if (next == '.')
                            next = 'L';
                        switch (next)
                        {
                            case 'L':
                                empty++;
                                break;
                            case '#':
                                occupied++;
                                break;
                        }
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
                var cent = seat;
                var next = backup[row][--cent];
                visited++;

                switch (next)
                {
                    case 'L':
                        empty++;
                        break;
                    case '.':
                        while (next == '.' && cent > 0)
                        {
                            next = backup[row][--cent];
                        }

                        if (next == '.')
                            next = 'L';
                        switch (next)
                        {
                            case 'L':
                                empty++;
                                break;
                            case '#':
                                occupied++;
                                break;
                        }
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
                var cent = seat;
                var cent2 = row;
                var next = backup[--cent2][--cent];
                visited++;

                switch (next)
                {
                    case 'L':
                        empty++;
                        break;
                    case '.':
                        while (next == '.' && cent > 0 && cent2 > 0)
                        {
                            next = backup[--cent2][--cent];
                        }

                        if (next == '.')
                            next = 'L';
                        switch (next)
                        {
                            case 'L':
                                empty++;
                                break;
                            case '#':
                                occupied++;
                                break;
                        }
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
                var cent = row;
                var next = backup[--cent][seat];
                visited++;

                switch (next)
                {
                    case 'L':
                        empty++;
                        break;
                    case '.':
                        while (next == '.' && cent > 0)
                        {
                            next = backup[--cent][seat];
                        }

                        if (next == '.')
                            next = 'L';
                        switch (next)
                        {
                            case 'L':
                                empty++;
                                break;
                            case '#':
                                occupied++;
                                break;
                        }
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
                var cent = seat;
                var cent2 = row;
                var next = backup[--cent2][++cent];
                visited++;

                switch (next)
                {
                    case 'L':
                        empty++;
                        break;
                    case '.':
                        while (next == '.' && cent < MaxSeats && cent2 > 0)
                        {
                            next = backup[--cent2][++cent];
                        }

                        if (next == '.')
                            next = 'L';
                        switch (next)
                        {
                            case 'L':
                                empty++;
                                break;
                            case '#':
                                occupied++;
                                break;
                        }
                        break;
                    case '#':
                        occupied++;
                        break;
                }
            }

            switch (currentSeat)
            {
                case 'L' when visited == empty:
                    {
                        var seats = backup[row];
                        var chars = seats.ToCharArray();
                        chars[seat] = '#';
                        var newArrangement = new string(chars);
                        backup[row] = newArrangement;
                        break;
                    }
                case '#' when occupied >= 5:
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

        private static bool SeatKeepsChanging(IEnumerable<string> previousSeating, IReadOnlyList<string> newSeating)
        {
            var i = 0;
            return previousSeating.Any(t => t != newSeating[i++]);
        }
    }
}
