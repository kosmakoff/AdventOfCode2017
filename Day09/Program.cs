using System;
using System.IO;
using Day09.Parser;
using Sprache;
using static Common.Utils;

namespace Day09
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 09");

            var input = File.ReadAllText("Input.txt");
            var group = Grammar.GroupParser.Parse(input);

            var groupsCountingVisitor = new GroupsCountingVisitor();
            group.Accept(groupsCountingVisitor);

            PrintAnswer("Problem 1 answer", groupsCountingVisitor.GroupsCount);

            var garbageVisitor = new GarbageCharactersCountingVisitor();
            group.Accept(garbageVisitor);

            PrintAnswer("Problem 2 answer", garbageVisitor.CharactersCount);
        }
    }
}
