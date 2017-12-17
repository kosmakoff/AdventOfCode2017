using System;
using System.Collections.Generic;
using static Common.Utils;

namespace Day17
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 17");

            var input = 376;

            int position = 0;
            var buffer = new List<int> {0};

            for (int i = 1; i <= 2017; i++)
            {
                position = (position + input) % buffer.Count;
                buffer.Insert(position+1, i);
                position++;
            }

            var answer1 = buffer[(position + 1) % buffer.Count];
            PrintAnswer("Problem 1 answer", answer1);

            position = 0;
            var afterZeroValue = 0;
            var length = 1;

            for (int value = 1; value <= 50_000_000; value++)
            {
                position = (position + input) % length;
                length++;
                if (position == 0)
                    afterZeroValue = value;
                position++;
            }

            PrintAnswer("Problem 2 answer", afterZeroValue);
        }
    }
}
