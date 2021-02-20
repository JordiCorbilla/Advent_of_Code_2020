using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    internal class Program
    {
        private static void Main()
        {
            SimpleProcessing();
            ComplexProcessing();
        }

        private static void ComplexProcessing()
        {
            var file = File.ReadAllLines("input.txt");

            var group = "";
            var totalVotes = 0;
            foreach (var line in file)
                if (line != "")
                {
                    @group += line + Environment.NewLine;
                }
                else
                {
                    var votes = ProcessComplexGroup(@group);
                    totalVotes += votes;
                    @group = "";
                }

            if (group != "")
            {
                var votes = ProcessComplexGroup(group);
                totalVotes += votes;
            }

            Console.WriteLine(totalVotes);
        }

        private static void SimpleProcessing()
        {
            var file = File.ReadAllLines("input.txt");

            var group = "";
            var totalVotes = 0;
            foreach (var line in file)
                if (line != "")
                {
                    @group += line;
                }
                else
                {
                    var votes = ProcessGroup(@group);
                    totalVotes += votes;
                    @group = "";
                }

            if (group != "")
            {
                var votes = ProcessGroup(group);
                totalVotes += votes;
            }

            Console.WriteLine(totalVotes);
        }

        private static int ProcessGroup(string group)
        {
            var hSet = new HashSet<char>(group);
            return hSet.Count;
        }

        private static int ProcessComplexGroup(string group)
        {
            var people = group.Split(Environment.NewLine);
            var numPeople = people.Length - 1;

            var answers = new Dictionary<char, int>();

            foreach (var person in people)
            {
                if (person == "") continue;
                foreach (var answer in person)
                    if (!answers.ContainsKey(answer))
                        answers.Add(answer, 1);
                    else
                        answers[answer]++;
            }

            return answers.Count(item => item.Value == numPeople);
        }
    }
}