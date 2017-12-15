using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static Common.Utils;

namespace Day15
{
    internal class Program
    {
        private static readonly Regex InputRegex = new Regex(@"^Generator (?:A|B) starts with (?<value>\d+)$");

        private const int GeneratorA = 16807;
        private const int GeneratorB = 48271;

        private static void Main(string[] args)
        {
            PrintHeader("Day 15");

            var input = File.ReadAllLines("Input.txt")
                .Select(s => InputRegex.Match(s).Groups["value"].Value)
                .Select(int.Parse)
                .ToList();

            var startValueA = input[0];
            var startValueB = input[1];

            var counter = GenerateRange(40_000_000, Generate(startValueA, GeneratorA), Generate(startValueB, GeneratorB))
                .Select(tuple => (maskedA: tuple.Item2 & 0xffff, maskedB: tuple.Item3 & 0xffff))
                .Count(tuple => tuple.maskedA == tuple.maskedB);

            PrintAnswer("Problem 1 answer", counter);

            counter = GenerateRange(5_000_000,
                    generatorA: Generate(startValueA, GeneratorA).Where(v => (v & 0b0011) == 0),
                    generatorB: Generate(startValueB, GeneratorB).Where(v => (v & 0b0111) == 0))
                .Select(tuple => (maskedA: tuple.Item2 & 0xffff, maskedB: tuple.Item3 & 0xffff))
                .Count(tuple => tuple.maskedA == tuple.maskedB);

            PrintAnswer("Problem 2 answer", counter);
        }

        private static IEnumerable<int> Generate(int start, int generator)
        {
            var current = start;
            while (true)
            {
                current = Multiply(current, generator);
                yield return current;
            }
        }

        private static IEnumerable<(int, int, int)> GenerateRange(int count, IEnumerable<int> generatorA, IEnumerable<int> generatorB)
        {
            return Enumerable.Range(0, count).Zip(generatorA.Zip(generatorB, (i1, i2) => (i1, i2)), (i, tuple) => (i, tuple.Item1, tuple.Item2));
        }

        private static int Multiply(int value, int generator)
        {
            return (int) ((long) value * generator % 2147483647);
        }
    }
}
