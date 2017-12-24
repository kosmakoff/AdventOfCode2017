namespace Common.Assembler.Instructions
{
    public class SubInstruction : IInstruction
    {
        public Register Register { get; }
        private readonly IValue _value;

        public SubInstruction(Register register, IValue value)
        {
            Register = register;
            _value = value;
        }

        public int Execute(Processor processor)
        {
            var value = processor.Memory.GetValue(Register.Name) - _value.Value;
            processor.Memory.SetValue(Register.Name, value);
            return 1;
        }

        public override string ToString()
        {
            return $"{Register} -= {_value}";
        }
    }
}