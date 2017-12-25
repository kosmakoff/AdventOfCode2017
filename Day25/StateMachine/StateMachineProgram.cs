using System.Collections.Generic;
using System.Linq;

namespace Day25.StateMachine
{
    internal class StateMachineProgram
    {
        public Prelude Prelude { get; }
        public IDictionary<StateMachineState, StateOperation> StateOperations { get; }

        public StateMachineProgram(Prelude prelude, IEnumerable<StateOperation> stateOperations)
        {
            Prelude = prelude;
            StateOperations = stateOperations.ToDictionary(so => so.State);
        }
    }
}
