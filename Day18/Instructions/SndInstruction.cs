using System;

namespace Day18.Instructions
{
    internal class SndInstruction : IInstruction
    {
        public IValue Value { get; }

        public SndInstruction(IValue value)
        {
            Value = value;
        }

        public int Execute(Processor processor)
        {
            processor.Send(Value.Value);
            return 1;
        }

        public override string ToString()
        {
            return $"SND {Value}";
        }
    }
}