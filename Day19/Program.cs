using System;
using System.IO;

namespace Day19
{
    public class Cycle
    {
        public Cycle Next { get; set; }
        public object Activator { get; set; }

        public string Consume(string input)
        {
            return input.Substring(1);
        }
    }

    public class OrCycle
    {
       // public 
    }
    class Program
    {
        static void Main(string[] args)
        {
            //0: 4 1 5
            //1: 2 3 | 3 2
            //2: 4 4 | 5 5
            //3: 4 5 | 5 4
            //4: "a"
            //5: "b"

            // Replace numbers
            var file = File.ReadAllLines("inputTest.txt");


            var four = new Cycle();
            var one = new Cycle();
            var five = new Cycle();


            four.Next = one;
            one.Next = five;

            four.Activator = "a";
            five.Activator = "b";
            one.Activator = 



        }
    }
}
