using System;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day13
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 13");

            var firewallItems = File.ReadAllLines("Input.txt")
                .Select(s => s.Split(": ").Select(int.Parse).ToArray())
                .Select(a => new FirewallItem(a[0], a[1]))
                .ToList();

            var severity = firewallItems
                .Select(i => CalculateSeverity(i, i.Depth, item => item.Depth * item.Range))
                .Sum();
            PrintAnswer("Problem 1 answer", severity);

            var maxDepth = firewallItems.Last().Depth;
            var depths = Enumerable.Range(0, maxDepth + 1);
            var depthsWithItems = depths
                .Select(depth => new
                {
                    Depth = depth,
                    Item = firewallItems.FirstOrDefault(item => item.Depth == depth)
                }).ToList();

            var safeDelay = 0;

            while (true)
            {
                var delayCopy = safeDelay;
                var isDangerous = depthsWithItems
                    .Select(a => CalculateSeverity(a.Item, a.Depth + delayCopy, item => 1))
                    .Any(s => s > 0);

                if (!isDangerous)
                    break;

                safeDelay++;
            }

            PrintAnswer("Problem 2 answer", safeDelay);
        }

        private static int CalculateSeverity(FirewallItem firewallItem, int pointInTime, Func<FirewallItem, int> scoreFunc)
        {
            if (firewallItem == null)
                return 0;

            var index = pointInTime;
            var doubleRange = firewallItem.Range * 2 - 2;
            var doublePosition = index % doubleRange;
            var position = doublePosition < firewallItem.Range
                ? doublePosition
                : doubleRange - doublePosition;

            return position == 0 ? scoreFunc(firewallItem) : 0;
        }


        private class FirewallItem
        {
            public int Depth { get; }

            public int Range { get; }

            public FirewallItem(int depth, int range)
            {
                Depth = depth;
                Range = range;
            }

            public override string ToString()
            {
                return $"{Depth}: {Range}";
            }
        }
    }
}
