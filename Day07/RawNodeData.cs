using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    internal class RawNodeData
    {
        private static readonly Regex Regex = new Regex(@"^(?<name>\w+) \((?<weight>\d+)\)( -> (?<subnodes>\w+(, \w+)+))?$");

        public string Name { get; private set; }

        public int Weight { get; private set; }

        public IEnumerable<string> Subnodes { get; private set; }

        public static RawNodeData Parse(string data)
        {
            var match = Regex.Match(data);
            if (!match.Success)
                throw new FormatException($"'{data}' is not a valid raw node data.");

            var name = match.Groups["name"].Value;
            var weight = int.Parse(match.Groups["weight"].Value);

            var subNodesGroup = match.Groups["subnodes"].Value;

            var subNodes = !string.IsNullOrWhiteSpace(subNodesGroup)
                ? subNodesGroup.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList()
                : null;

            return new RawNodeData
            {
                Name = name,
                Weight = weight,
                Subnodes = subNodes
            };
        }

        private sealed class NameEqualityComparer : IEqualityComparer<RawNodeData>
        {
            public bool Equals(RawNodeData x, RawNodeData y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Name, y.Name);
            }

            public int GetHashCode(RawNodeData obj)
            {
                return obj.Name != null ? obj.Name.GetHashCode() : 0;
            }
        }

        public static IEqualityComparer<RawNodeData> NameComparer { get; } = new NameEqualityComparer();
    }
}
