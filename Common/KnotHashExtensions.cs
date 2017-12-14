using System.Linq;

namespace Common
{
    public static class KnotHashExtensions
    {
        public static byte[] CondenceKnotHash(this byte[] input)
        {
            return input
                .Window(16)
                .Select(w => w.Aggregate((byte) 0, (sum, current) => (byte) (sum ^ current)))
                .ToArray();
        }
    }
}
