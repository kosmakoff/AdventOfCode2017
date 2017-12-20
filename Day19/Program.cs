using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Common.Utils;

namespace Day19
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 19");

            var map = File.ReadAllLines("Input.txt");

            var startColumn = map[0].IndexOf('|');

            int row = 0, column = startColumn;

            var strBuilder = new StringBuilder();

            var stepsCount = 1;

            foreach ((int row, int column) step in EnumerateSteps(map, row, column))
            {
                stepsCount++;

                row += step.row;
                column += step.column;

                var charAtStep = map[row][column];
                if (IsAlpha(charAtStep))
                    strBuilder.Append(charAtStep);
            }

            PrintAnswer("Problem 1 answer", strBuilder.ToString());
            PrintAnswer("Problem 2 answer", stepsCount);
        }

        private static IEnumerable<(int, int)> EnumerateSteps(string[] map, int startRow, int startColumn)
        {
            var currentRow = startRow;
            var currentColumn = startColumn;

            bool isHorizontal = false;
            (int dRow, int dCol) currentDirection = (1, 0);

            currentRow = currentRow + currentDirection.dRow;
            currentColumn = currentColumn + currentDirection.dCol;

            do
            {
                var charAtPos = map[currentRow][currentColumn];
                if (IsAlpha(charAtPos) || IsPipe(charAtPos) || IsCross(charAtPos))
                {
                    yield return currentDirection;
                }

                if (IsCross(charAtPos))
                {
                    if (isHorizontal)
                    {
                        isHorizontal = false;

                        if (IsSafe(map, currentRow - 1, currentColumn) &&
                            IsWalkable(map, currentRow - 1, currentColumn))
                            currentDirection = (-1, 0);
                        else
                            currentDirection = (1, 0);
                    }
                    else
                    {
                        isHorizontal = true;

                        if (IsSafe(map, currentRow, currentColumn - 1) &&
                            IsWalkable(map, currentRow, currentColumn - 1))
                            currentDirection = (0, -1);
                        else
                            currentDirection = (0, 1);
                    }
                }

                currentRow = currentRow + currentDirection.dRow;
                currentColumn = currentColumn + currentDirection.dCol;
            } while (IsSafe(map, currentRow, currentColumn) && IsWalkable(map, currentRow, currentColumn));
        }

        private static bool IsSafe(string[] map, int row, int column)
        {
            return row >= 0 && row < map.Length && column >= 0 && column < map[0].Length;
        }

        private static bool IsWalkable(string[] map, int row, int column)
        {
            var @char = map[row][column];
            return IsWalkable(@char);
        }

        private static bool IsWalkable(char @char)
        {
            return IsAlpha(@char) || IsPipe(@char) || IsCross(@char);
        }

        private static bool IsAlpha(char @char)
        {
            return @char >= 'a' && @char <= 'z' || @char >= 'A' && @char <= 'Z';
        }

        private static bool IsPipe(char @char)
        {
            return @char == '|' || @char == '-';
        }

        private static bool IsCross(char @char)
        {
            return @char == '+';
        }
    }
}
