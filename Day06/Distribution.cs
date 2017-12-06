using System;
using System.Collections.Generic;
using System.Linq;

namespace Day06
{
    internal class Distribution : IEqualityComparer<Distribution>
    {
        public int[] Data { get; }

        public Distribution(int[] data)
        {
            Data = data;
        }

        public Distribution Redistribute()
        {
            var newData = new int[Data.Length];
            Array.Copy(Data, newData, Data.Length);

            var maxItemPosition = newData
                .Select((value, pos) => (pos, value))
                .Aggregate((maxPos: -1, maxValue: -1),
                    ((int maxPos, int maxValue) max, (int pos, int value) current) => current.value > max.maxValue
                        ? (maxPos: current.pos, maxValue: current.value)
                        : max)
                .maxPos;

            var blocksToDistribute = newData[maxItemPosition];
            newData[maxItemPosition] = 0;

            foreach (var dataIndex in EnumerateRoundaboutIndexes(maxItemPosition + 1, newData.Length))
            {
                if (blocksToDistribute == 0)
                    break;

                newData[dataIndex] += 1;
                blocksToDistribute--;
            }

            return new Distribution(newData);
        }
        
        private static IEnumerable<int> EnumerateRoundaboutIndexes(int start, int max)
        {
            var currentIndex = start % max;

            while (true)
            {
                yield return currentIndex++ % max;
            }
        }

        protected bool Equals(Distribution other)
        {
            return Data.SequenceEqual(other.Data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Distribution) obj);
        }

        public override int GetHashCode()
        {
            var result = 0;
            var shift = 0;
            foreach (var d in Data)
            {
                shift = (shift + 11) % 21;
                result ^= (d + 1024) << shift;
            }
            return result;
        }

        public bool Equals(Distribution x, Distribution y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Distribution obj)
        {
            return obj.GetHashCode();
        }

        public override string ToString()
        {
            return string.Join(", ", Data);
        }
    }
}
