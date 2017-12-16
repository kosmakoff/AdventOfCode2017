using System.Collections.Generic;
using System.Linq;

namespace Day16.Commands
{
    internal sealed class ExchangeCommand : ICommand
    {
        public int Index1 { get; }
        public int Index2 { get; }

        public ExchangeCommand(int index1, int index2)
        {
            Index1 = index1;
            Index2 = index2;
        }

        public (int[], IDictionary<char, char>) Execute((int[], IDictionary<char, char>) state)
        {
            (var moves, var substitutions) = state;
            var copy = moves.ToArray();
            var temp = copy[Index1];
            copy[Index1] = copy[Index2];
            copy[Index2] = temp;
            return (copy, substitutions);
        }
    }
}
