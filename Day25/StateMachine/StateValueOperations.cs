namespace Day25.StateMachine
{
    internal class StateValueOperations
    {
        public StateValueOperations(bool writeValue, Direction tapeShiftDirection, StateMachineState continueWithState)
        {
            WriteValue = writeValue;
            TapeShiftDirection = tapeShiftDirection;
            ContinueWithState = continueWithState;
        }

        public bool WriteValue { get; }
        public Direction TapeShiftDirection { get; }
        public StateMachineState ContinueWithState { get; }
    }
}
