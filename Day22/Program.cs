using System;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day22
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 22");

            var input = File.ReadAllLines("Input.txt");
            var width = input[0].Length;
            var height = input.Length;
            var middleWidth = width / 2;
            var middleHeight = height / 2;

            var data1 = input.SelectMany(s => s).Select(c => c == '#').ToArray();

            var map1 = new Map<bool>(width, height, data1);

            Run(map1, Direction.Up, (r: middleHeight, c: middleWidth),
                () => new BoolCellStateManager(), 10000, out int infectionsCount);

            PrintAnswer("Problem 1 answer", infectionsCount);

            var data2 = input.SelectMany(s => s).Select(c => c == '#' ? CellState.Infected : CellState.Clean).ToArray();

            var map2 = new Map<CellState>(width, height, data2);

            Run(map2, Direction.Up, (r: middleHeight, c: middleWidth), () => new CellStateManager(),
                10_000_000, out int infectionsCount2);
            
            PrintAnswer("Problem 2 answer", infectionsCount2);
        }

        private static void Run<T>(Map<T> map, Direction startDirection, (int r, int c) startPosition,
            Func<CellStateManagerBase<T>> managerFactory, int iterationCount, out int infectionsCount)
        {
            var currentDirection = startDirection;
            var currentPosition = startPosition;

            infectionsCount = 0;
            var manager = managerFactory();

            for (int i = 0; i < iterationCount; i++)
            {
                RunOneVirusCycle(map, ref currentDirection, ref currentPosition, ref infectionsCount, manager);
            }
        }

        private static void RunOneVirusCycle<T>(Map<T> map, ref Direction currentDirection, ref (int r, int c) currentPosition, ref int infectionsCount, CellStateManagerBase<T> cellStateManager)
        {
            (var r, var c) = currentPosition;
            var state = map[r, c];

            var changeDirectionFunction = cellStateManager.MutateDirection(state);
            currentDirection = changeDirectionFunction(currentDirection);

            state = cellStateManager.MutateState(state);
            map[r, c] = state;

            if (cellStateManager.IsInfected(state))
                infectionsCount++;

            currentPosition = ChangeCoords(currentPosition, currentDirection);
        }

        private static (int r, int c) ChangeCoords((int r, int c) currentCoords, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return (currentCoords.r - 1, currentCoords.c);
                case Direction.Down:
                    return (currentCoords.r + 1, currentCoords.c);
                case Direction.Left:
                    return (currentCoords.r, currentCoords.c - 1);
                case Direction.Right:
                    return (currentCoords.r, currentCoords.c + 1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static char FormatCellState(CellState cellState)
        {
            switch (cellState)
            {
                case CellState.Clean:
                    return '.';
                case CellState.Weakened:
                    return 'W';
                case CellState.Infected:
                    return '#';
                case CellState.Flagged:
                    return 'F';
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellState), cellState, null);
            }
        }
    }
}
