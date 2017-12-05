using System;
using System.IO;
using System.Linq;

namespace Day05
{
    internal class InstructionsMemory
    {
        private readonly bool _isStrange;
        private readonly int[] _instructions;
        private int _pointer;

        public InstructionsMemory(int[] instructions, bool isStrange = false)
        {
            _isStrange = isStrange;
            _instructions = new int[instructions.Length];
            Array.Copy(instructions, _instructions, instructions.Length);
        }

        public InstructionsMemory(string fileName, bool isStrange = false)
            : this(File.ReadAllLines(fileName).Select(int.Parse).ToArray(), isStrange)
        {
        }

        public bool FollowInstruction()
        {
            var offset = _instructions[_pointer];

            if (_isStrange && offset >= 3)
                _instructions[_pointer]--;
            else
                _instructions[_pointer]++;

            _pointer += offset;
            return _pointer < 0 || _pointer >= _instructions.Length;
        }
    }
}
