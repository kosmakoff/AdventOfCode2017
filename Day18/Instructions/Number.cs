namespace Day18.Instructions
{
    internal class Number : IValue
    {
        public long Value { get; }

        public Number(long value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
