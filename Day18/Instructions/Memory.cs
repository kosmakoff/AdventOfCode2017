using System.Collections.Generic;

namespace Day18.Instructions
{
    internal class Memory
    {
        private readonly IDictionary<char, long> _memory;

        public Memory()
        {
            _memory = new Dictionary<char, long>();
        }

        public void SetValue(char name, long value)
        {
            _memory[name] = value;
        }

        public long GetValue(char name)
        {
            return _memory.TryGetValue(name, out var value) ? value : 0;
        }
    }
}
