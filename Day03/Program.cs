using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static Common.Utils;

namespace Day03
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 03");

            const int input = 325489;
            (var x, var y) = CalculatePlainCoords(input);

            var result1 = Math.Abs(x) + Math.Abs(y);

            PrintAnswer("Problem 1 answer", result1);

            // problem 2

            var result2 = EnumerateFibonacciSpiral().SkipWhile(i => i < input).First();
            PrintAnswer("Problem 2 answer", result2);
        }

        private static (int x, int y) CalculatePlainCoords(int n)
        {
            // based on calculations at https://stackoverflow.com/a/41350703/2267059
            var k = (int) Math.Ceiling((Math.Sqrt(n) - 1) / 2);
            var t = 2 * k + 1;
            var m = t * t;

            t -= 1;

            if (n >= m - t)
            {
                return (-k, k - (m - n));
            }

            m -= t;

            if (n >= m - t)
            {
                return (-k + (m - n), -k);
            }

            m -= t;

            if (n >= m - t)
            {
                return (k, -k + (m - n));
            }

            return (k - (m - n - t), k);
        }

        private static IEnumerable<int> EnumerateFibonacciSpiral()
        {
            IDictionary<Coords, int> cache = new Dictionary<Coords, int>()
            {
                [new Coords(0, 0)] = 1
            };

            var position = new Coords(0, 0);

            foreach (var move in EnumerateMoves())
            {
                position = position + move;

                // gather neigbors
                var sum = TryGetItem(position + new Coords(1, 0), cache) +
                          TryGetItem(position + new Coords(1, 1), cache) +
                          TryGetItem(position + new Coords(0, 1), cache) +
                          TryGetItem(position + new Coords(-1, 1), cache) +
                          TryGetItem(position + new Coords(-1, 0), cache) +
                          TryGetItem(position + new Coords(-1, -1), cache) +
                          TryGetItem(position + new Coords(0, -1), cache) +
                          TryGetItem(position + new Coords(1, -1), cache);

                cache[position] = sum;

                yield return sum;
            }
        }

        private static int TryGetItem(Coords position, IDictionary<Coords, int> cache)
        {
            return cache.TryGetValue(position, out var value) ? value : 0;
        }

        private static IEnumerable<Coords> EnumerateMoves()
        {
            // RU | LLDD | RRRUUU | LLLLDDDD

            int moves = 1;

            while (true)
            {
                // R
                for (var i = 0; i < moves; i++)
                {
                    yield return new Coords(1, 0);
                }

                // L
                for (var i = 0; i < moves; i++)
                {
                    yield return new Coords(0, 1);
                }

                moves++;

                // L
                for (var i = 0; i < moves; i++)
                {
                    yield return new Coords(-1, 0);
                }

                // D
                for (var i = 0; i < moves; i++)
                {
                    yield return new Coords(0, -1);
                }

                moves++;
            }
        }

        private class Coords : IEqualityComparer<Coords>
        {
            public int X { get; }

            public int Y { get; }

            public Coords(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static Coords operator +(Coords a, Coords b)
            {
                return new Coords(a.X + b.X, a.Y + b.Y);
            }

            private bool Equals(Coords other)
            {
                return X == other.X && Y == other.Y;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Coords) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (X * 397) ^ Y;
                }
            }

            public bool Equals(Coords x, Coords y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(Coords obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
