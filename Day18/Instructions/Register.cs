namespace Day18.Instructions
{
    internal class Register : IValue
    {
        private readonly Memory _memory;
        public char Name { get; }
        public long Value => _memory.GetValue(Name);

        public Register(char name, Memory memory)
        {
            _memory = memory;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name} ({Value})";
        }
    }
}
