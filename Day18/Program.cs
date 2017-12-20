using System.Collections.Generic;
using System.IO;
using System.Linq;
using Day18.Instructions;
using static Common.Utils;

namespace Day18
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 18");

            var parser = new InstructionParser();

            List<IInstruction> InstructionsFactory(Memory mem) => File.ReadAllLines("Input.txt")
                .Select(instruction => parser.Parse(instruction, mem))
                .ToList();

            var memory = new Memory();
            var queue = new Queue<long>();
            var instructions = InstructionsFactory(memory);

            var processor = new Processor(instructions, memory, queue, queue);

            bool isStop = false;

            processor.AfterInstructionRan += (sender, instruction) =>
            {
                switch (instruction)
                {
                    case RcvInstruction _:
                        isStop = true;
                        break;
                }
            };

            do
            {
                processor.RunOne();
            } while (!isStop);

            PrintAnswer("Problem 1 answer", queue.Last());
            
            var queue12 = new Queue<long>();
            var queue21 = new Queue<long>();

            var memory1 = new Memory();
            var instructions1 = InstructionsFactory(memory1);
            var processor1 = new Processor(0, instructions1, memory1, queue21, queue12);

            var memory2 = new Memory();
            var instructions2 = InstructionsFactory(memory2);
            var processor2 = new Processor(1, instructions2, memory2, queue12, queue21);

            bool proc1StepOk, proc2StepOk, proc1Waiting, proc2Waiting;

            var sendCounter = 0;
            processor2.AfterInstructionRan += (sender, instruction) =>
            {
                if (instruction is SndInstruction)
                    sendCounter++;
            };

            do
            {
                (proc1StepOk, proc1Waiting) = processor1.RunOne();
                (proc2StepOk, proc2Waiting) = processor2.RunOne();

            } while ((proc1StepOk || proc2StepOk || queue12.Any() || queue21.Any()) && !(proc1Waiting && proc2Waiting));

            PrintAnswer("Problem 2 answer", sendCounter);
        }
    }
}
