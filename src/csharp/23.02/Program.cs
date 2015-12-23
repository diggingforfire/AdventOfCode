using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _23._02
{
    class Program
    {
        static readonly Dictionary<char, int> registers = new Dictionary<char, int>();
        private static List<Instruction> instructions;

        static void Main(string[] args)
        {
            instructions = File.ReadAllLines("input.txt")
                .Select(line => line.Split(' '))
                .Select(parts => new Instruction
                {
                    Type = (InstructionType)Enum.Parse(typeof(InstructionType), parts[0], true),
                    Register = char.IsLetter(parts[1][0]) ? parts[1][0] : (char?)null,
                    Operand = parts.Length == 2 && !char.IsLetter(parts[1][0]) ? int.Parse(parts[1]) : parts.Length == 3 ? int.Parse(parts[2]) : (int?)null
                })
                .ToList();

            instructions.Where(i => i.Register.HasValue).Select(i => i.Register.Value).Distinct().ToList().ForEach(r => registers.Add(r, 0));
            registers['a'] = 1;

            var next = Process(instructions[0]);

            while (next != null)
                next = Process(next);

            Console.WriteLine(registers['b']);
            Console.ReadKey();
        }

        static Instruction Process(Instruction instruction)
        {
            int offset = 1;

            switch (instruction.Type)
            {
                case InstructionType.Hlf:
                    registers[instruction.Register.Value] /= 2;
                    break;
                case InstructionType.Tpl:
                    registers[instruction.Register.Value] *= 3;
                    break;
                case InstructionType.Inc:
                    registers[instruction.Register.Value]++;
                    break;
                case InstructionType.Jmp:
                    offset = instruction.Operand.Value;
                    break;
                case InstructionType.Jie:
                    offset = (registers[instruction.Register.Value] & 1) == 0 ? instruction.Operand.Value : 1;
                    break;
                case InstructionType.Jio:
                    offset = registers[instruction.Register.Value] == 1 ? instruction.Operand.Value : 1;
                    break;
            }

            if (instructions.IndexOf(instruction) + offset < instructions.Count)
                return instructions[instructions.IndexOf(instruction) + offset];

            return null;
        }

        class Instruction
        {
            public InstructionType Type { get; set; }
            public char? Register { get; set; }
            public int? Operand { get; set; }
        }

        enum InstructionType
        {
            Hlf,
            Tpl,
            Inc,
            Jmp,
            Jie,
            Jio
        }

    }
}
