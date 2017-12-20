namespace Day18.Instructions
{
    internal class JgzInstruction : IInstruction
    {
        private readonly IValue _value;
        private readonly IValue _shiftValue;

        public JgzInstruction(IValue value, IValue shiftValue)
        {
            _value = value;
            _shiftValue = shiftValue;
        }

        public int Execute(Processor processor)
        {
            return _value.Value > 0 ? (int) _shiftValue.Value : 1;
        }

        public override string ToString()
        {
            return $"if {_value} > 0 : jump by {_shiftValue}";
        }
    }
}