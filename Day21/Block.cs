using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    internal class Block
    {
        private static readonly IDictionary<int, int> SquareRoots =
            Enumerable.Range(1, 10000).ToDictionary(a => a * a, a => a);

        private readonly BitArray _data;

        public int Size { get; }

        public bool[] Data => _data.Cast<bool>().ToArray();

        public Block(bool[] data)
        {
            if (!SquareRoots.Keys.Contains(data.Length))
                throw new ArgumentException("Data length is not a square of natural number");

            _data = new BitArray(data);
            Size = SquareRoots[_data.Length];
        }

        public Block(BitArray data)
        {
            if (!SquareRoots.Keys.Contains(data.Length))
                throw new ArgumentException("Data length is not a square of natural number");

            _data = new BitArray(data);
            Size = SquareRoots[_data.Length];
        }

        /// <summary>
        /// Extracts sub Block from this block
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Block GetSubBlock(int row, int column, int size)
        {
            if (row + size > Size || column + size > Size)
                throw new InvalidOperationException("Can't get subblock with given parameters");

            var bits = new BitArray(size * size);

            for (int r = row; r < row + size; r++)
            {
                for (int c = column; c < column + size; c++)
                {
                    var oldCoords = r * Size + c;
                    var newCoords = (r - row) * size + (c - column);

                    bits[newCoords] = _data[oldCoords];
                }
            }

            return new Block(bits);
        }

        public static Block MergeBlocks(List<Block> subblocks)
        {
            if (!SquareRoots.Keys.Contains(subblocks.Count))
                throw new ArgumentException("Subblocks length is not a square of natural number");

            var subblockSize = subblocks[0].Size;
            if (subblocks.Any(b => b.Size != subblockSize))
                throw new ArgumentException("All subblocks must be the same size");

            var smallSize = SquareRoots[subblocks.Count];
            var size = smallSize * subblockSize;

            var bits = new BitArray(size * size);

            for (int bigR = 0; bigR < smallSize; bigR++)
            {
                for (int bigC = 0; bigC < smallSize; bigC++)
                {
                    var blockIndex = bigR * smallSize + bigC;

                    for (int r = 0; r < subblockSize; r++)
                    {
                        for (int c = 0; c < subblockSize; c++)
                        {
                            var innerIndex = r * subblockSize + c;

                            var outerR = bigR * subblockSize + r;
                            var outerC = bigC * subblockSize + c;
                            var outerIndex = outerR * size + outerC;

                            bits[outerIndex] = subblocks[blockIndex]._data[innerIndex];
                        }
                    }
                }
            }

            return new Block(bits);
        }

        public List<Block> SplitBlocks(int size)
        {
            return EnumerateSplitBlocks(size).ToList();
        }

        private IEnumerable<Block> EnumerateSplitBlocks(int size)
        {
            if (Size % size != 0)
                throw new ArgumentException("Size must divide block's size evenly");

            var steps = Size / size;

            for (int r = 0; r < steps; r++)
            for (int c = 0; c < steps; c++)
            {
                yield return GetSubBlock(r * size, c * size, size);
            }
        }

        private IEnumerable<BitArray> EnumerateRotations()
        {
            for (int i = 0; i < 4; i++)
            {
                yield return Rotate(_data, i);
                yield return Flip(Rotate(_data, i));
            }
        }

        /// <summary>
        /// Flips data vertically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static BitArray Flip(BitArray data)
        {
            var size = SquareRoots[data.Length];
            var result = (BitArray)data.Clone();

            for (int c = 0; c < size / 2; c++)
            {
                var columnA = c;
                var columnB = size - 1 - c;

                for (int r = 0; r < size; r++)
                {
                    var indexA = r * size + columnA;
                    var indexB = r * size + columnB;

                    var temp = result[indexA];
                    result[indexA] = result[indexB];
                    result[indexB] = temp;
                }
            }

            return result;
        }

        /// <summary>
        /// Rotates data by 90 degrees
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        private BitArray Rotate(BitArray data, int rotation)
        {
            if (rotation == 0)
            {
                return (BitArray)data.Clone();
            }

            var size = SquareRoots[data.Length];
            var result = new BitArray(data.Length);

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    var originalIndex = r * size + c;
                    int newIndex;

                    int newR, newC;

                    switch (rotation)
                    {
                        case 1:
                            newR = c;
                            newC = size - 1 - r;
                            newIndex = newR * size + newC;
                            break;
                        case 2:
                            newIndex = data.Length - 1 - originalIndex;
                            break;
                        case 3:
                            newR = size -1 - c;
                            newC = r;
                            newIndex = newR * size + newC;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(rotation));
                    }

                    result[newIndex] = data[originalIndex];
                }
            }

            return result;
        }

        private sealed class BlockEqualityComparer : IEqualityComparer<Block>
        {
            public bool Equals(Block x, Block y)
            {
                if (x._data.Length != y._data.Length)
                    return false;

                var lowestScoreX = x.EnumerateRotations()
                    .Select(data => new {Data = data, Score = Score(data)})
                    .OrderBy(a => a.Score)
                    .First().Data;

                var lowestScoreY = y.EnumerateRotations()
                    .Select(data => new { Data = data, Score = Score(data) })
                    .OrderBy(a => a.Score)
                    .First().Data;

                return lowestScoreX.Cast<bool>().SequenceEqual(lowestScoreY.Cast<bool>());
            }

            private static int Score(BitArray data)
            {
                return data
                    .Cast<bool>()
                    .Select((b, i) => new {V = b ? 1 : 0, I = i})
                    .Select(a => a.V * (1 << a.I))
                    .Sum();
            }

            public int GetHashCode(Block obj)
            {
                var lowestScoreData = obj.EnumerateRotations()
                    .Select(data => new {Data = data, Score = Score(data)})
                    .OrderBy(a => a.Score)
                    .First().Data;

                return lowestScoreData.Cast<bool>().Aggregate(17, (current, t) => current * 23 + t.GetHashCode());
            }
        }

        public static IEqualityComparer<Block> BlockComparer { get; } = new BlockEqualityComparer();
    }
}
