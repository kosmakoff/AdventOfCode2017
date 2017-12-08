using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day08
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 08");

            var instructions = File.ReadAllLines("Input.txt")
                .Select(Instruction.Parse);
            
            var memory = new Memory();
            var largestValueEver = 0;

            void OnMemoryOnDataSet(object sender, KeyValuePair<string, int> pair)
            {
                largestValueEver = pair.Value > largestValueEver ? pair.Value : largestValueEver;
            }

            memory.DataSet += OnMemoryOnDataSet;

            foreach (var instruction in instructions)
            {
                instruction.Execute(memory);
            }

            memory.DataSet -= OnMemoryOnDataSet;

            var largestValue = memory.Data.Values.Max();
            PrintAnswer("Problem 1 answer", largestValue);
            PrintAnswer("Problem 2 answer", largestValueEver);
        }
    }
}
