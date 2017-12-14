using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class KnotHashCalculator
    {
        public static byte[] MagicSuffix = {17, 31, 73, 47, 23};

        public static byte[] Calculate(byte[] input, int dataLength = 256, int roundsCount = 64)
        {
            var data = Enumerable.Range(0, dataLength).Select(i => (byte)i).ToArray();

            var position = 0;
            var skipSize = 0;

            for (int counter = 0; counter < roundsCount; counter++)
            {
                (position, skipSize) = RunSingleRound(input, data, position, skipSize);
            }

            return data;
        }

        public static byte[] Calculate(byte[] input, byte[] suffix, int dataLength = 256, int roundsCount = 64)
        {
            var newInput = new byte[input.Length + suffix.Length];
            Array.Copy(input, 0, newInput, 0, input.Length);
            Array.Copy(suffix, 0, newInput, input.Length, suffix.Length);
            return Calculate(newInput, dataLength, roundsCount);
        }

        private static (int position, int skipSize) RunSingleRound(byte[] lengths, byte[] data, int position, int skipSize)
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
