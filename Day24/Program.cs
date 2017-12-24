using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day24
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 24");

            var input = File.ReadAllLines("Input.txt");
            var links = input.Select(Link.Parse).ToList();

            var maxBridgeWeight = EnumerateBridges(0, links)
                .Select(a => a.Sum(l => l.LeftSide + l.RightSide))
                .Max();

            PrintAnswer("Problem 1 answer", maxBridgeWeight);

            var maxLengthMaxBridgeWeight = EnumerateBridges(0, links)
                .Select(bridge => bridge.ToList())
                .Select(a => new {Length = a.Count, Weight = a.Sum(link => link.LeftSide + link.RightSide)})
                .GroupBy(a => a.Length, a => a.Weight)
                .OrderByDescending(grp => grp.Key)
                .First()
                .Max();

            PrintAnswer("Problem 2 answer", maxLengthMaxBridgeWeight);
        }

        private static IEnumerable<IEnumerable<Link>> EnumerateBridges(int start, List<Link> pool)
        {
            var roots = pool
                .Where(link => link.LeftSide == start || link.RightSide == start)
                .Select(link => link.Rotate(start));

            foreach (var root in roots)
            {
                yield return new[] {root};

                var subBridges = EnumerateBridges(root.RightSide, pool.Where(l => l.Guid != root.Guid).ToList());
                foreach (var subBridge in subBridges)
                {
                    yield return new[] {root}.Concat(subBridge);
                }
            }
        }
    }
}
