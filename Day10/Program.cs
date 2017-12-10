using System;
using System.Collections.Generic;
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
            int answer1 = SolveProblem1(input);
            PrintAnswer("Problem 1 answer", answer1);

            var inputBytes = Encoding.ASCII.GetBytes(Input).ToList();
            inputBytes.AddRange(new byte[] {17, 31, 73, 47, 23});

            var answer2 = SolveProblem2(inputBytes);
            PrintAnswer("Problem 2 answer", answer2);
        }

        private static int SolveProblem1(IList<byte> lengths, int dataLength = 256)
        {
            var data = Enumerable.Range(0, dataLength).Select(i => (byte)i).ToArray();

            var position = 0;
            var skipSize = 0;

            /*(position, skipSize) = */ RunRound(lengths, data, position, skipSize);

            var answer1 = data[0] * data[1];
            return answer1;
        }

        private static string SolveProblem2(IList<byte> lengths, int dataLength = 256)
        {
            var data = Enumerable.Range(0, dataLength).Select(i => (byte)i).ToArray();

            var position = 0;
            var skipSize = 0;

            for (int counter = 0; counter < 64; counter++)
            {
                (position, skipSize) = RunRound(lengths, data, position, skipSize);
            }

            var hashSums = data.Window(16)
                .Select(w => w.Aggregate((byte)0, (sum, current) => (byte)(sum ^ current)));

            return string.Join(string.Empty, hashSums.Select(b => b.ToString("x2")));
        }

        private static (int position, int skipSize) RunRound(IList<byte> lengths, byte[] data, int position, int skipSize)
        {
            var currentPosition = position;
            var currentSkipSize = skipSize;

            foreach (var length in lengths)
            {
                Permutate(data, length, currentPosition);
                currentPosition = (currentPosition + length + currentSkipSize) % data.Length;
                currentSkipSize++;
            }
            return (currentPosition, currentSkipSize);
        }

        private static void Permutate(byte[] data, int length, int position)
        {
            // shortcut
            if (length == 1)
                return;
            int index1, index2, count;
            for (index1 = position, index2 = (position + length - 1) % data.Length, count = 0;
                count < length / 2;
                index1 = ++index1 % data.Length, index2 = (--index2 + data.Length) % data.Length, count++)
            {
                SwapTwoValues(data, index1, index2);
            }
        }

        private static void SwapTwoValues(IList<byte> data, int index1, int index2)
        {
            var temp = data[index1];
            data[index1] = data[index2];
            data[index2] = temp;
        }
    }
}
