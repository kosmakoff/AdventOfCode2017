using System;
using System.Collections.Generic;
using System.Linq;
using static Common.Utils;

namespace Day06
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 06");

            const string inputString = "5	1	10	0	1	7	13	14	3	12	8	10	7	12	0	6";
            var input = inputString.Split('\t', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var distribution = new Distribution(input);

            var hashSet = new HashSet<Distribution>();
            var list = new List<Distribution>();

            var steps = 0;
            int firstOccurrenceIndex;

            while (true)
            {
                distribution = distribution.Redistribute();
                steps++;
                list.Add(distribution);
                if (!hashSet.Add(distribution))
                {
                    firstOccurrenceIndex = list.FindIndex(d => d.Equals(distribution)) + 1;
                    break;
                }
            }

            PrintAnswer("Problem 1 answer", steps);
            PrintAnswer("Problem 2 answer", steps - firstOccurrenceIndex);
        }
    }
}
