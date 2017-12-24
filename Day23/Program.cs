using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Assembler;
using Common.Assembler.Instructions;
using static Common.Utils;

namespace Day23
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 23");

            var parser = new InstructionParser();

            List<IInstruction> InstructionsFactory(Memory mem) => File.ReadAllLines("Input.txt")
                .Select(instruction => parser.Parse(instruction, mem))
                .ToList();

            var memory = new Memory();
            var queue = new Queue<long>();
            var instructions = InstructionsFactory(memory);

            bool proc1StepOk;
            int mulInstructionsCounter = 0;

            var processor = new Processor(instructions, memory, queue, queue);

            processor.AfterInstructionRan += (sender, instruction) =>
            {
                if (instruction is MulInstruction)
                {
                    mulInstructionsCounter++;
                }
            };

            do
            {
                (proc1StepOk, _) = processor.RunOne();

            } while (proc1StepOk);

            PrintAnswer("Problem 1 answer", mulInstructionsCounter);

            // Answer 2 was received by manual analysis of the program
            // The result is amount of non-prime numbers from 109300 to 126300 with step 17
            var answer2 = Enumerable.Range(0, 1001)
                .Select(x => 109300 + x * 17)
                .Count(x => !IsPrime(x));
            
            PrintAnswer("Problem 2 answer", answer2);
        }

        private static bool IsPrime(int number)
        {
            var root = (int) Math.Sqrt(number);
            return (Enumerable.Range(2, root)).All(v => number % v != 0);
        }
    }
}
