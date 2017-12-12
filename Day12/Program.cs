using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day12
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 12");

            var input = File.ReadAllLines("Input.txt");
            var linksDictionary = input.Select(Link.Parse)
                .ToDictionary(a => a.Id, a => a.LinkedIds);

            var groups = new Dictionary<int, HashSet<int>>();

            foreach (var id in linksDictionary.Keys)
            {
                if (groups.Values.Any(list => list.Contains(id)))
                    continue;

                var group = new HashSet<int> { id };
                groups[id] = group;

                var queue = new Queue<int>();
                queue.Enqueue(id);

                while (queue.TryDequeue(out var subId))
                {
                    var moreIds = linksDictionary[subId];

                    group.Add(subId);

                    foreach (var moreId in moreIds)
                    {
                        if (!group.Contains(moreId))
                            queue.Enqueue(moreId);
                    }
                }
            }

            PrintAnswer("Problem 1 answer", groups[0].Count);
            PrintAnswer("Problem 2 answer", groups.Count);
        }
    }
}
