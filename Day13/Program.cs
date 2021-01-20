using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

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
    }
    class Program
    {
        static void Main()
        {
            //Part1();
            //Console.WriteLine();
            //Part2();

            long pos = 100633400000000;
            while (true)
            {
                var b = (pos + 36) % 37;
                if (b == 0)
                {
                    var c = (pos + 42) % 557;
                    if (c == 0)
                    {
                        var d = (pos + 44) % 29;
                        if (d == 0)
                        {
                            var e = (pos + 55) % 13;
                            if (e == 0)
                            {
                                var f = (pos + 59) % 17;
                                if (f == 0)
                                {
                                    var g = (pos + 65) % 23;
                                    if (g == 0)
                                    {
                                        var h = (pos + 73) % 419;
                                        if (h == 0)
                                        {
                                            var i = (pos + 92) % 19;
                                            if (i == 0)
                                            {
                                                Console.WriteLine(pos);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                pos += 67;

                if (pos % 100000000 == 0)
                {
                    Console.WriteLine($"{pos}");
                }
            }
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

            var busesLong = new List<int>();
            foreach (var bus in buses)
            {
                if (bus != "x")
                {
                    busesLong.Add(int.Parse(bus));
                }
                else
                {
                    busesLong.Add(-1);
                }
            }

            long pos = 100333920000000;

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
                var m = new StringBuilder();
                for (var i = 0; i < busesLong.Count; i++)
                {
                    if (buses[i] == "x") continue;
                    m.Append(pos % busesLong[i] == 0 ? "D" : ".");
                }

                memory.AddLast(m.ToString());

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
                if (pos % 10000000 != 0) continue;
                sp.Stop();
                Console.WriteLine($"{pos} - {sp.ElapsedMilliseconds}ms");
                sp = Stopwatch.StartNew();
            }
        }

        private static bool Compare(IEnumerable<string> item, string snapshot)
        {
            var one = item.Aggregate("", (current, s) => current + (s + "-"));
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
