namespace Common.Assembler.Instructions
{
    public class RcvInstruction : IInstruction
    {
        public Register Register { get; }

        public RcvInstruction(Register register)
        {
            Register = register;
        }

        public int Execute(Processor processor)
        {
            if (processor.Receive(out var value))
            {
                processor.Memory.SetValue(Register.Name, value);
                return 1;
            }

            return 0;
        }

        public override string ToString()
        {
            return $"RCV {Register}";
        }
    }
}