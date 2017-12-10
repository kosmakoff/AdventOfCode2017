using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> SlidingWindow<T>(this IEnumerable<T> source, int windowLength)
        {
            if (windowLength < 1)
                throw new ArgumentOutOfRangeException(nameof(windowLength), "Must be greater or equal to 1");

            Queue<T> buffer = new Queue<T>(windowLength);

            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (buffer.Count == windowLength)
                    {
                        yield return buffer.ToArray();
                        buffer.Dequeue();
                    }

                    buffer.Enqueue(enumerator.Current);
                }

                if (buffer.Count == windowLength)
                {
                    yield return buffer.ToArray();
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Window<T>(this IEnumerable<T> source, int windowLength)
        {
            if (windowLength < 1)
                throw new ArgumentOutOfRangeException(nameof(windowLength), "Must be greater or equal to 1");

            using (var enumerator = source.GetEnumerator())
            {
                while (true)
                {
                    List<T> list = enumerator.Take(windowLength).ToList();
                    if (list.Count == windowLength)
                        yield return list;
                    else
                        yield break;
                }
            }
        }

        public static Func<T, T> Identity<T>()
        {
            return t => t;
        }

        public static IEnumerable<T> Take<T>(this IEnumerator<T> source, int count)
        {
            var counter = 0;

            while (counter < count && source.MoveNext())
            {
                counter++;

                yield return source.Current;
            }
        }
    }
}
