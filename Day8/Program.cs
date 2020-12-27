using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Day8
{
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
            switch(operation)
            {
                case "nop": Operation = InstructionOperation.nop;
                    break;
                case "acc":
                    Operation = InstructionOperation.acc;
                    break;
                case "jmp":
                    Operation = InstructionOperation.jmp;
                    break;
            }

            value = value.Replace("+", "");
            Value = int.Parse(value);
            Visited = false;
        }
    }
    public class Program
    {
        public static int Accumulator { get; set; }
        
        static void Main(string[] args)
        {
            var file = File.ReadAllLines("inputTest.txt");
            var listInstructions = new List<Instruction>();
            foreach (var row in file)
            {
                var ins = row.Split(" ");
                var instruction = new Instruction(ins[0], ins[1]);
                listInstructions.Add(instruction);
            }

            int index = 0;
            while (true)
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
        }
    }
}
