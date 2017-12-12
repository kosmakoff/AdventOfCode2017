using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day12
{
    internal class Link
    {
        private static readonly Regex LinkRegex = new Regex(@"^(?<id>\d+) \<-\> (?<ids>\d+(, \d+)*)$");

        public int Id { get; }

        public IList<int> LinkedIds { get; }

        public Link(int id, IList<int> linkedIds)
        {
            Id = id;
            LinkedIds = linkedIds;
        }

        public static Link Parse(string linkString)
        {
            var match = LinkRegex.Match(linkString);
            if (!match.Success)
                throw new FormatException($"'{linkString}' is not in valid format");

            var id = int.Parse(match.Groups["id"].Value);
            var ids = match.Groups["ids"].Value.Split(", ").Select(int.Parse).ToList();

            return new Link(id, ids);
        }
    }
}
