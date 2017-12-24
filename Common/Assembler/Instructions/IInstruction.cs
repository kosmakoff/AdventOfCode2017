namespace Common.Assembler.Instructions
{
    public interface IInstruction
    {
        int Execute(Processor processor);
    }
}
