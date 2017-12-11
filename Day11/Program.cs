using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day11
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 11");

            var steps = File.ReadAllText("Input.txt").Split(',');
            var stepsAggregated = steps.Aggregate(
                (max: 0, coords: new Coords()),
                (tuple, move) =>
                {
                    var newCoords = tuple.coords.Move(move);
                    var newMax = newCoords.Distance > tuple.max ? newCoords.Distance : tuple.max;
                    return (max: newMax, coords: newCoords);
                });

            PrintAnswer("Problem 1 answer", stepsAggregated.coords.Distance);
            PrintAnswer("Problem 2 answer", stepsAggregated.max);
        }

        private static int GetSteps(IDictionary<string, int> dict, string key)
        {
            if (dict.TryGetValue(key, out var count))
                return count;
            return 0;
        }
    }

    internal class Coords
    {
        public int DownRight { get; }

        public int DownLeft { get; }

        public Coords()
        {
        }

        public Coords(int downLeft, int downRight)
        {
            DownLeft = downLeft;
            DownRight = downRight;
        }

        public int Distance 
        {
            get
            {
                var sameSign = DownRight == 0 || DownLeft == 0 || DownRight > 0 && DownLeft > 0 ||
                               DownRight < 0 && DownLeft < 0;

                return sameSign
                    ? Math.Max(Math.Abs(DownRight), Math.Abs(DownLeft))
                    : Math.Abs(DownRight) + Math.Abs(DownLeft);
            }
        }

        public Coords Move(string move)
        {
            switch (move)
            {
                case "n":
                    return MoveN();
                case "ne":
                    return MoveNE();
                case "se":
                    return MoveSE();
                case "s":
                    return MoveS();
                case "sw":
                    return MoveSW();
                case "nw":
                    return MoveNW();
                default:
                    throw new ArgumentOutOfRangeException(nameof(move));
            }
        }

        public Coords MoveN() => new Coords(DownLeft - 1, DownRight - 1);

        public Coords MoveNE() => new Coords(DownLeft - 1, DownRight);

        public Coords MoveSE() => new Coords(DownLeft, DownRight + 1);

        public Coords MoveS() => new Coords(DownLeft + 1, DownRight + 1);

        public Coords MoveSW() => new Coords(DownLeft + 1, DownRight);

        public Coords MoveNW() => new Coords(DownLeft, DownRight - 1);
    }
}
