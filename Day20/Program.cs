using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day20
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 20");

            var pointDataList = File.ReadAllLines("Input.txt")
                .Select(PointData.Parse)
                .Select((pd, i) => new {Data = pd, Index = i})
                .ToList();

            var problem1Answer = pointDataList
                .OrderBy(pd => pd.Data.Acceleration.ManhattanDistance)
                .ThenBy(pd => pd.Data.Velocity.ManhattanDistance)
                .ThenBy(pd => pd.Data.Position.ManhattanDistance)
                .First()
                .Index;

            PrintAnswer("Problem 1 answer", problem1Answer);

            for (int iter = 0; iter < 100; iter++)
            {
                pointDataList = pointDataList.GroupBy(a => a.Data.Position, Vector.XYZComparer)
                    .Where(grp => grp.Count() == 1)
                    .SelectMany(grp => grp)
                    .ToList();

                for (int i = 0; i < pointDataList.Count; i++)
                {
                    var pointData = pointDataList[i].Data;
                    pointData.Velocity += pointData.Acceleration;
                    pointData.Position += pointData.Velocity;
                }
            }

            PrintAnswer("Problem 2 answer", pointDataList.Count);
        }
    }
}
