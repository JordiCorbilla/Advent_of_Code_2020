using System;
using System.IO;
using System.Linq;

namespace Day8
{
    // ReSharper disable all InconsistentNaming
    public enum InstructionOperation
    {
        nop,
        acc,
        jmp
    }

    public class Instruction
    {
        public InstructionOperation Operation { get; set; }
        public int Value { get; set; }
        public bool Visited { get; set; }

        public Instruction(string operation, string value)
        {
            Operation = operation switch
            {
                "nop" => InstructionOperation.nop,
                "acc" => InstructionOperation.acc,
                "jmp" => InstructionOperation.jmp,
                _ => Operation
            };

            value = value.Replace("+", "");
            Value = int.Parse(value);
            Visited = false;
        }
    }
    public class Program
    {
        public static int Accumulator { get; set; }
        
        static void Main()
        {
            var file = File.ReadAllLines("inputfix.txt");
            var listInstructions = file.Select(row => row.Split(" "))
                .Select(ins => new Instruction(ins[0], ins[1])).ToList();

            var index = 0;
            var maxInstructions = listInstructions.Count;
            while (index != maxInstructions)
            {
                var op = listInstructions[index];
                if (op.Visited)
                {
                    break;
                }

                switch (op.Operation)
                {
                    case InstructionOperation.acc:
                        Accumulator += op.Value;
                        index++;
                        break;
                    case InstructionOperation.jmp:
                        index += op.Value;
                        break;
                    case InstructionOperation.nop:
                        index++;
                        break;
                }

                op.Visited = true;
            }

            Console.WriteLine(Accumulator);

            foreach (var instruction in listInstructions)
            {
                string visited = "[x]";
                string notVisited = "[ ]";
                string output;
                if (instruction.Visited)
                    output = $"{visited} {instruction.Operation}:{instruction.Value}";
                else
                    output = $"{notVisited} {instruction.Operation}:{instruction.Value}";
                Console.WriteLine(output);
            }

            Console.WriteLine(Accumulator);
        }
    }
}
