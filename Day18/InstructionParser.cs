using System;
using Common.Assembler;
using Common.Assembler.Instructions;

namespace Day18
{
    internal class InstructionParser
    {
        public IInstruction Parse(string instruction, Memory memory)
        {
            var cmd = instruction.Substring(0, 3);
            var args = instruction.Substring(4).Split(' ');

            Register register;
            IValue value;

            switch (cmd)
            {
                case "set":
                    register = new Register(args[0][0], memory);

                    if (args[1][0] >= 'a' && args[1][0] <= 'z')
                        value = new Register(args[1][0], memory);
                    else
                        value = new Number(long.Parse(args[1]));

                    return new SetInstruction(register, value);
                case "add":
                    register = new Register(args[0][0], memory);

                    if (args[1][0] >= 'a' && args[1][0] <= 'z')
                        value = new Register(args[1][0], memory);
                    else
                        value = new Number(long.Parse(args[1]));

                    return new AddInstruction(register, value);
                case "mod":
                    register = new Register(args[0][0], memory);

                    if (args[1][0] >= 'a' && args[1][0] <= 'z')
                        value = new Register(args[1][0], memory);
                    else
                        value = new Number(long.Parse(args[1]));

                    return new ModInstruction(register, value);
                case "mul":
                    register = new Register(args[0][0], memory);

                    if (args[1][0] >= 'a' && args[1][0] <= 'z')
                        value = new Register(args[1][0], memory);
                    else
                        value = new Number(long.Parse(args[1]));

                    return new MulInstruction(register, value);
                case "jgz":
                    if (args[0][0] >= 'a' && args[0][0] <= 'z')
                        value = new Register(args[0][0], memory);
                    else
                        value = new Number(long.Parse(args[0]));

                    IValue value2;
                    if (args[1][0] >= 'a' && args[1][0] <= 'z')
                        value2 = new Register(args[1][0], memory);
                    else
                        value2 = new Number(long.Parse(args[1]));

                    return new JgzInstruction(value, value2);
                case "snd":
                    if (args[0][0] >= 'a' && args[0][0] <= 'z')
                        value = new Register(args[0][0], memory);
                    else
                        value = new Number(long.Parse(args[0]));

                    return new SndInstruction(value);
                case "rcv":
                    register = new Register(args[0][0], memory);
                    return new RcvInstruction(register);
                default:
                    throw new ArgumentOutOfRangeException(nameof(instruction), $"Instruction {instruction} is not supported.");
            }
        }
    }
}
