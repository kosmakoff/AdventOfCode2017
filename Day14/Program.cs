using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using static Common.Utils;

namespace Day14
{
    internal class Program
    {
        private const string Input = "oundnydw";

        private static void Main(string[] args)
        {
            PrintHeader("Day 14");

            var parallelQuery = Enumerable.Range(0, 128)
                .AsParallel().AsOrdered()
                .Select(i => $"{Input}-{i}")
                .Select(s => Encoding.ASCII.GetBytes(s))
                .Select(bs => KnotHashCalculator.Calculate(bs, KnotHashCalculator.MagicSuffix).CondenceKnotHash())
                .ToList();

            var answer1 = parallelQuery
                .Select(CalculateOnes)
                .Sum();

            PrintAnswer("Problem 1 answer", answer1);

            var onesMap = parallelQuery
                .Select(ConvertToZeroOnes)
                .Select(enumerable => enumerable.ToArray())
                .ToArray();

            var answer2 = CountIslands(onesMap);
            PrintAnswer("Problem 2 answer", answer2);
        }

        /// <summary>
        /// Counts islands of 1s, where cells may only connect vertically or horizontally
        /// Algorithm was shamelessly stolen from http://www.geeksforgeeks.org/find-number-of-islands/
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private static int CountIslands(bool[][] map)
        {
            var rows = map.Length;
            var columns = map[0].Length;

            var visited = new bool[rows, columns];
            int count = 0;

            for (int row = 0; row < rows; row++)
            for (int column = 0; column < columns; column++)
            {
                if (map[row][column] && !visited[row, column])
                {
                    DFS(map, row, column, visited);
                    count++;
                }
            }

            return count;
        }

        private static readonly int[] RowShifts = {+1, -1, 0, 0};
        private static readonly int[] ColumnShifts = {0, 0, +1, -1};

        private static void DFS(bool[][] map, int row, int column, bool[,] visited)
        {
            visited[row, column] = true;

            for (int k = 0; k < 4; k++)
            {
                if (IsSafe(map, row + RowShifts[k], column + ColumnShifts[k], visited))
                    DFS(map, row + RowShifts[k], column + ColumnShifts[k], visited);
            }
        }

        private static bool IsSafe(bool[][]map, int row, int column, bool[,] visited)
        {
            var rows = map.Length;
            var columns = map[0].Length;

            return row >= 0 && row < rows && column >= 0 && column < columns && map[row][column] && !visited[row, column];
        }

        private static IEnumerable<bool> ConvertToZeroOnes(IEnumerable<byte> arg)
        {
            return arg.SelectMany(ConvertToZeroOnes);
        }

        private static IEnumerable<bool> ConvertToZeroOnes(byte arg)
        {
            yield return (arg & 0b10000000) == 0b10000000;
            yield return (arg & 0b01000000) == 0b01000000;
            yield return (arg & 0b00100000) == 0b00100000;
            yield return (arg & 0b00010000) == 0b00010000;
            yield return (arg & 0b00001000) == 0b00001000;
            yield return (arg & 0b00000100) == 0b00000100;
            yield return (arg & 0b00000010) == 0b00000010;
            yield return (arg & 0b00000001) == 0b00000001;
        }

        private static int CalculateOnes(byte[] arg)
        {
            return arg.Select(CalculateOnes).Sum();
        }

        private static int CalculateOnes(byte arg)
        {
            return ConvertToZeroOnes(arg).Count(a => a);
        }
    }
}
