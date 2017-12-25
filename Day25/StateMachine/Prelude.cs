namespace Day25.StateMachine
{
    internal class Prelude
    {
        public Prelude(StateMachineState startState, int diagnosticChecksumIterations)
        {
            StartState = startState;
            DiagnosticChecksumIterations = diagnosticChecksumIterations;
        }

        public StateMachineState StartState { get; }
        public int DiagnosticChecksumIterations { get; }
    }
}
