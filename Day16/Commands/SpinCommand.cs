using System.Collections.Generic;
using System.Linq;

namespace Day16.Commands
{
    internal sealed class SpinCommand : ICommand
    {
        public int Count { get; }

        public SpinCommand(int count)
        {
            Count = count;
        }

        public (int[], IDictionary<char, char>) Execute((int[], IDictionary<char, char>) state)
        {
            (var moves, var substitutions) = state;
            var count = Count % moves.Length;

            if (count == 0)
                return (moves, substitutions);

            var newMoves = moves.Skip(moves.Length - count).Take(count).Concat(moves.Take(moves.Length - count))
                .ToArray();

            return (newMoves, substitutions);
        }
    }
}
