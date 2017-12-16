using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Common;
using Day16.Commands;
using static Common.Utils;

namespace Day16
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 16");

            var commandParser = new CommandParser();
            var commands = File.ReadAllText("Input.txt").Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(commandParser.Parse)
                .ToList();

            var data = "abcdefghijklmnop";

            var answer1 = Calculate(commands, data, rounds: 1);
            var answer2 = Calculate(commands, data, rounds: 1_000_000_000);

            PrintAnswer("Problem 1 answer", answer1);
            PrintAnswer("Problem 2 answer", answer2);
        }

        private static string Calculate(IEnumerable<ICommand> commands, string data, int rounds = 1)
        {
            (var moves, var substitutions) = Compile(commands, data.ToList());
            var bits = GetBits(rounds);

            var maps = new (int[], IDictionary<char, char>)[32];

            var currentState = (moves, substitutions);
            maps[0] = currentState;

            for (int i = 0; i < 32; i++)
            {
                maps[i] = currentState;
                currentState = Permutate(currentState, currentState);
            }

            (moves, substitutions) = maps
                .Zip(bits, (m, b) => new { Map = m, Bit = b })
                .Where(a => a.Bit)
                .Select(a => a.Map)
                .Aggregate(Permutate);

            var result = moves.Select(i => substitutions[data[i]]).ToArray();

            return new string(result);
        }

        private static (int[] moves, IDictionary<char, char> substitiutions) Permutate(
            (int[] moves, IDictionary<char, char> substitiutions) state1,
            (int[] moves, IDictionary<char, char> substitiutions) state2)
        {
            (var moves1, var substitutions1) = state1;
            (var moves2, var substitutions2) = state2;

            var newMoves = moves1.Select(move => moves2[move]).ToArray();
            var newSubstitutions = substitutions1.ToDictionary(kvp => kvp.Key, kvp => substitutions2[kvp.Value]);

            return (newMoves, newSubstitutions);
        }

        private static (int[] moves, IDictionary<char, char> substitiutions) Compile(IEnumerable<ICommand> commands, IReadOnlyList<char> data)
        {
            var moves = Enumerable.Range(0, data.Count).ToArray();
            IDictionary<char, char> substitutions = data.ToDictionary(c => c, c => c);

            foreach (var command in commands)
            {
                (moves, substitutions) = command.Execute((moves, substitutions));
            }

            return (moves, substitutions);
        }

        private static IEnumerable<bool> GetBits(int number)
        {
            var result = new bool[32];

            for (var i = 0; i < 32; i++)
            {
                var flag = 1 << i;
                result[i] = (number & flag) == flag;
            }

            return result;
        }
    }
}
