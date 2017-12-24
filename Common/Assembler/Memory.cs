using System.Collections;
using System.Collections.Generic;

namespace Common.Assembler
{
    public class Memory : IEnumerable<KeyValuePair<char, long>>
    {
        private readonly IDictionary<char, long> _memory;

        public Memory()
        {
            _memory = new SortedDictionary<char, long>();
        }

        public void SetValue(char name, long value)
        {
            _memory[name] = value;
        }

        public long GetValue(char name)
        {
            return _memory.TryGetValue(name, out var value) ? value : 0;
        }

        public IEnumerator<KeyValuePair<char, long>> GetEnumerator()
        {
            return _memory.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
