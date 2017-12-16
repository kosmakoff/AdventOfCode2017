using System.Collections.Generic;
using System.Linq;

namespace Day16.Commands
{
    internal sealed class PartnerCommand : ICommand
    {
        public char Char1 { get; }
        public char Char2 { get; }

        public PartnerCommand(char char1, char char2)
        {
            Char1 = char1;
            Char2 = char2;
        }

        public (int[], IDictionary<char, char>) Execute((int[], IDictionary<char, char>) state)
        {
            (var moves, var substitutions) = state;

            var newSubstitutions = substitutions.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var kvp in substitutions)
            {
                if (kvp.Value == Char1)
                    newSubstitutions[kvp.Key] = Char2;
                if (kvp.Value == Char2)
                    newSubstitutions[kvp.Key] = Char1;
            }

            return (moves, newSubstitutions);
        }
    }
}
