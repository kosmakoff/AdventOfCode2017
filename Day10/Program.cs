using System;
using static Common.Utils;
using System.Linq;
using System.Text;
using Common;

namespace Day10
{
    internal class Program
    {
        private const string Input = "76,1,88,148,166,217,130,0,128,254,16,2,130,71,255,229";

        private static void Main(string[] args)
        {
            PrintHeader("Day 10");

            var input = Input.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(byte.Parse).ToArray();
            var hash1 = KnotHashCalculator.Calculate(input, roundsCount: 1);
            PrintAnswer("Problem 1 answer", hash1[0] * hash1[1]);

            var inputBytes = Encoding.ASCII.GetBytes(Input);
            var hash2 = KnotHashCalculator.Calculate(inputBytes, KnotHashCalculator.MagicSuffix).CondenceKnotHash();
            var hashStr = string.Join(string.Empty, hash2.Select(b => b.ToString("x2")));
            PrintAnswer("Problem 2 answer", hashStr);
        }
    }
}
