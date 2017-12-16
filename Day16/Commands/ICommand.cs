using System.Collections.Generic;

namespace Day16.Commands
{
    internal interface ICommand
    {
        (int[], IDictionary<char, char>) Execute((int[], IDictionary<char, char>) state);
    }
}