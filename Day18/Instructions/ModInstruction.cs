namespace Day18.Instructions
{
    internal class ModInstruction : IInstruction
    {
        private readonly Register _register;
        private readonly IValue _value;

        public ModInstruction(Register register, IValue value)
        {
            _register = register;
            _value = value;
        }

        public int Execute(Processor processor)
        {
            var value = processor.Memory.GetValue(_register.Name) % _value.Value;
            processor.Memory.SetValue(_register.Name, value);
            return 1;
        }

        public override string ToString()
        {
            return $"{_register} %= {_value}";
        }
    }
}