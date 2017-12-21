using System;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day21
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 21");

            var mappingRules = File.ReadAllLines("Input.txt")
                    .Select(line => line.Split(" => ", StringSplitOptions.RemoveEmptyEntries))
                    .Select(parts => new {Left = ParseBlock(parts[0]), Right = ParseBlock(parts[1])})
                    .ToDictionary(a => a.Left, a => a.Right, Block.BlockComparer);

            var block = new Block(new[] {false, true, false, false, false, true, true, true, true});

            int onPixels5 = 0, onPixels18 = 0;

            for (int i = 1; i <= 18; i++)
            {
                if (block.Size % 2 == 0)
                {
                    block = Block.MergeBlocks(block.SplitBlocks(2)
                        .Select(b => mappingRules[b]).ToList());
                }
                else if (block.Size % 3 == 0)
                {
                    block = Block.MergeBlocks(block.SplitBlocks(3)
                        .Select(b => mappingRules[b]).ToList());
                }

                if (i == 5)
                {
                    onPixels5 = block.Data.Count(b => b);
                }

                if (i == 18)
                {
                    onPixels18 = block.Data.Count(b => b);
                }
            }

            PrintAnswer("Problem 1 answer", onPixels5);
            PrintAnswer("Problem 2 answer", onPixels18);
        }

        private static Block ParseBlock(string data)
        {
            var bools = data.Replace("/", string.Empty)
                .Select(c => c == '#')
                .ToArray();

            return new Block(bools);
        }
    }
}
