using System.Collections.Generic;
using Sprache;

namespace Day09.Parser
{
    internal static class Grammar
    {
        // dirty hack - I could not figure out how to ignore the escaped char properly
        private static readonly Parser<char> EscapedGarbageParser =
            from bang in Parse.Char('!')
            from @char in Parse.AnyChar
            select '\0';

        private static readonly Parser<char> AnyGarbageChar =
            EscapedGarbageParser.Or(Parse.AnyChar);

        private static readonly Parser<IEnumerable<char>> GarbageString =
            AnyGarbageChar.Except(Parse.Char('>')).Many();

        private static readonly Parser<Garbage> GarbageParser =
            from opening in Parse.Char('<').Once()
            from content in GarbageString.Text()
            from closing in Parse.Char('>').Once()
            select new Garbage(content.Replace("\0", string.Empty));

        public static readonly Parser<Group> GroupParser =
            from opening in Parse.Char('{')
            from subGroups in GarbageParser.Or<GroupItem>(Parse.Ref(() => GroupParser))
                .DelimitedBy(Parse.Char(','))
                .Optional()
            from closing in Parse.Char('}')
            select new Group(subGroups.GetOrDefault());
    }
}
