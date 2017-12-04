using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using static Common.Utils;

namespace Day04
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 04");

            var input = File.ReadAllLines("Input.txt");

            var result1 = input
                .Count(IsPassphraseCorrect1);

            PrintAnswer("Problem 1 answer", result1);

            var result2 = input
                .Count(IsPassphraseCorrect2);

            PrintAnswer("Problem 2 answer", result2);
        }

        private static bool IsPassphraseCorrect1(string passphrase)
        {
            var words = passphrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var uniqueWords = new HashSet<string>(words, StringComparer.InvariantCultureIgnoreCase);
            return words.Length == uniqueWords.Count;
        }

        private static bool IsPassphraseCorrect2(string passphrase)
        {
            var words = passphrase.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(SortLetters).ToList();

            var uniqueWords = new HashSet<string>(words, StringComparer.InvariantCultureIgnoreCase);
            return words.Count == uniqueWords.Count;
        }

        private static string SortLetters(string arg)
        {
            return new string(arg.OrderBy(LinqExtensions.Identity<char>()).ToArray());
        }
    }
}
