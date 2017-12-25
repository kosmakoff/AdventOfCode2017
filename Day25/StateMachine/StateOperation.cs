namespace Day25.StateMachine
{
    internal class StateOperation
    {
        public StateOperation(StateMachineState state, StateValueOperations operationsOnZero, StateValueOperations operationsOnOne)
        {
            State = state;
            OperationsOnZero = operationsOnZero;
            OperationsOnOne = operationsOnOne;
        }

        public StateMachineState State { get; }
        public StateValueOperations OperationsOnZero { get; }
        public StateValueOperations OperationsOnOne { get; }
    }
}
