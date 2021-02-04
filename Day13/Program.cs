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
            var file = File.ReadAllLines("input.txt");
            var buses = file[1].Split(',');
            for (int i = 0; i < buses.Length; i++)
            {
                if (buses[i] != "x")
                {
                    Console.WriteLine($"(pos + {i}) % {buses[i]}");
                }
            }

            var file2 = File.ReadAllLines("inputtest6.txt");
            var buses2 = file2[1].Split(',');
            for (int i = 0; i < buses2.Length; i++)
            {
                if (buses2[i] != "x")
                {
                    Console.WriteLine($"(pos + {i}) % {buses2[i]}");
                }
            }

            Part2Test();
            Part2New();
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

        //getting close 743520161713899
        //yayyyyyy!
        //743520161713899

        private static void Part2New()
        {
            //         743520161713899
            long pos = 979999999999990;
            while (true)
            {
                var b = (pos + 35) % 37; // 1147
                if (b == 0)
                {
                    var c = (pos + 41) % 557; // 13925
                    if (c == 0)
                    {
                        var d = (pos + 43) % 29; // 667
                        if (d == 0)
                        {
                            var e = (pos + 54) % 13; // 156
                            if (e == 0)
                            {
                                var f = (pos + 58) % 17; // 136
                                if (f == 0)
                                {
                                    var g = (pos + 64) % 23; // 46
                                    if (g == 0)
                                    {
                                        var h = (pos + 72) % 419; // 2514
                                        if (h == 0)
                                        {
                                            Console.WriteLine($"getting close {pos}");
                                            var i = (pos + 91) % 19; // 475
                                            if (i == 0)
                                            {
                                                Console.WriteLine($"yayyyyyy!");
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

                pos += 41;

                if (pos % 100000000 == 0)
                {
                    Console.WriteLine($"{pos}");
                }
            }
        }

        private static void Part2Test()
        {
            long pos = 0;
            while (true)
            {
                var b = (pos + 1) % 37; // 1147
                if (b == 0)
                {
                    var c = (pos + 2) % 47; // 13925
                    if (c == 0)
                    {
                        var d = (pos + 3) % 1889; // 667
                        if (d == 0)
                        {
                            Console.WriteLine(pos);
                            break;
                        }
                    }
                }

                pos += 1789;

                if (pos % 100000 == 0)
                {
                    Console.WriteLine($"{pos}");
                }
            }
        }
    }
}
