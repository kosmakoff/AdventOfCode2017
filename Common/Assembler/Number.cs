namespace Common.Assembler
{
    public class Number : IValue
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
