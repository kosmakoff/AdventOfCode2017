using System;
using System.Collections.Generic;
using Common.Assembler.Instructions;
using Common;

namespace Common.Assembler
{
    public class Processor
    {
        private int _instructionPointer;

        private readonly int _id;
        private readonly IList<IInstruction> _instructionsList;
        private readonly Queue<long> _incomingQueue;
        private readonly Queue<long> _outgoingQueue;
        public Memory Memory { get; }

        public event EventHandler<IInstruction> AfterInstructionRan;

        public Processor(int id, IList<IInstruction> instructionsList, Memory memory, Queue<long> incomingQueue, Queue<long> outgoingQueue)
        {
            _instructionsList = instructionsList;
            _incomingQueue = incomingQueue;
            _outgoingQueue = outgoingQueue;

            Memory = memory;
            _instructionPointer = 0;

            _id = id;
            Memory.SetValue('p', id);
        }

        public Processor(IList<IInstruction> instructionsList, Memory memory, Queue<long> incomingQueue, Queue<long> outgoingQueue)
            : this(0, instructionsList, memory, incomingQueue, outgoingQueue)
        {
        }

        public (bool isOk, bool isWaiting) RunOne()
        {
            var instruction = _instructionsList[_instructionPointer];

            var shift = instruction.Execute(this);
            OnAfterInstructionRan(instruction);

            _instructionPointer += shift;

            return (_instructionPointer >= 0 && _instructionPointer < _instructionsList.Count, shift == 0);
        }

        public void Send(long value)
        {
            _outgoingQueue.Enqueue(value);
        }

        public bool Receive(out long value)
        {
            return _incomingQueue.TryDequeue(out value);
        }

        protected virtual void OnAfterInstructionRan(IInstruction instruction)
        {
            AfterInstructionRan?.Invoke(this, instruction);
        }
    }
}
