using System;
using System.IO;
using System.Linq;
using Common;
using Day25.StateMachine;
using Sprache;
using static Common.Utils;

namespace Day25
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 25");

            var input = File.ReadAllText("Input.txt");
            var program = Grammar.StateMachineProgramParser.Parse(input);

            var tape = new BinaryTape();
            var position = 0;
            var state = program.Prelude.StartState;

            for (int i = 0; i < program.Prelude.DiagnosticChecksumIterations; i++)
            {
                var operation = tape[position]
                    ? program.StateOperations[state].OperationsOnOne
                    : program.StateOperations[state].OperationsOnZero;

                ApplyOperation(operation, tape, ref position, out state);
            }

            var answer1 = tape.Count(LinqExtensions.Identity<bool>());
            PrintAnswer("Problem 1 answer", answer1);
        }

        private static void ApplyOperation(StateValueOperations operation, BinaryTape tape, ref int position,
            out StateMachineState state)
        {
            tape[position] = operation.WriteValue;
            if (operation.TapeShiftDirection == Direction.Left)
                position--;
            else
                position++;
            state = operation.ContinueWithState;
        }
    }
}
