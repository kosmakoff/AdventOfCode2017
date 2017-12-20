namespace Day18.Instructions
{
    internal class SetInstruction : IInstruction
    {
        private readonly Register _register;
        private readonly IValue _value;

        public SetInstruction(Register register, IValue value)
        {
            _register = register;
            _value = value;
        }

        public int Execute(Processor processor)
        {
            processor.Memory.SetValue(_register.Name, _value.Value);
            return 1;
        }

        public override string ToString()
        {
            return $"{_register} <- {_value}";
        }
    }
}
