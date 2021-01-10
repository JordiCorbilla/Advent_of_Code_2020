using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day13
{
    public class Bus {
        public int TimeStamp { get; set; }
        public bool Marked { get; set; }
        public int T { get; set; }
        public bool M { get; set; }

        public Bus(string pos, long index)
        {
            TimeStamp = int.Parse(pos);
            if (index % TimeStamp == 0)
                Marked = true;
        }

        public Bus(string pos, long index, int t)
        {
            TimeStamp = int.Parse(pos);
            if (index % TimeStamp == 0)
                Marked = true;
            T = t;
        }
    }
    class Program
    {
        static void Main()
        {
            Part1();
            Console.WriteLine();
            Part2();
        }

        /// <summary>
        /// For this part, I first generate the solution (what we are trying to look for) and call it snapshot
        /// e.g.:
        /// 
        /// D...
        /// .D..
        /// ..D.
        /// ...D
        ///
        /// Then, I allow the system to run and when I find that the last bus of the list is due to stop, I compare the
        /// current snapshot against the solution. If they match, then we found what we were looking for.
        /// </summary>
        private static void Part2()
        {
            var file = File.ReadAllLines("input.txt");
            var buses = file[1].Split(',');
            long pos = 100000460000000;

            //Generate Snapshot
            var items = buses.Where(x => x != "x").ToList().Count;
            var r = 0;
            var snapshot = new List<string>();
            foreach (var b in buses)
            {
                var a = new string('.', items).ToCharArray();
                
                if (b != "x")
                {
                    a[r] = 'D';
                    for (var i = 0; i < buses.Length; i++)
                    {
                        if (buses[i] == "x") continue;
                        if (snapshot.Count == int.Parse(buses[i]))
                        {
                            a[i] = 'D';
                        }
                    }
                    
                    snapshot.Add(new string(a));
                    r++;
                }
                else
                {
                    snapshot.Add(new string((a)));
                }
            }

            var snapshotFlat = "";
            foreach (var s in snapshot) 
                snapshotFlat += (s + "-");

            var memory = new LinkedList<string>();
            Stopwatch sp = Stopwatch.StartNew();

            while (true)
            {
                var m = "";
                for (var i = 0; i < buses.Length; i++)
                {
                    if (buses[i] == "x") continue;
                    var b = new Bus(buses[i], pos, i);
                    if (b.Marked)
                    {
                        m += "D";
                    }
                    else
                    {
                        m += ".";
                    }
                }
            
                memory.AddLast(m);

                if (memory.Count == snapshot.Count)
                {
                    if (memory.First.Value == snapshot[0] && memory.Last.Value == snapshot[^1]) //reduce compare hit
                    {
                        var found = pos;
                        if (Compare(memory, snapshotFlat))
                        {
                            Console.WriteLine($"Found in {(found - snapshot.Count) + 1}");
                            break;
                        }
                    }
                    memory.RemoveFirst();
                }

                pos++;
                if (pos % 10000000 == 0)
                {
                    sp.Stop();
                    Console.WriteLine($"{pos} - {sp.ElapsedMilliseconds}ms");
                    sp = Stopwatch.StartNew();
                }
            }
        }

        private static bool Compare(IEnumerable<string> item, string snapshot)
        {
            var one = "";
            foreach (var s in item) one += (s + "-");
            return one == snapshot;
        }

        private static void Part1()
        {
            var file = File.ReadAllLines("input.txt");
            var timestamp = long.Parse(file[0]);
            var initialTimestamp = timestamp;
            var buses = file[1].Split(',').Where(x => x != "x");
            var schedule = new List<List<Bus>>();
            var enumerable = buses as string[] ?? buses.ToArray();
            timestamp += 50;
            for (long i = 0; i < timestamp; i++)
            {
                var busesList = enumerable.Select(t => new Bus(t, i)).ToList();
                schedule.Add(busesList);
            }

            var trueFound = 0;
            var busLine = 0;
            for (var i = timestamp - 50; i < timestamp; i++)
            {
                var s = "";
                foreach (var p in schedule[(int) i])
                {
                    s += $"{p.TimeStamp}({p.Marked}), ";
                    if (!p.Marked || trueFound != 0) continue;
                    trueFound = (int)i;
                    busLine = p.TimeStamp;
                }
                Console.WriteLine($"{i}, {s}");
            }

            Console.WriteLine($"Solution = {(trueFound - initialTimestamp)*busLine}");
        }
    }
}
