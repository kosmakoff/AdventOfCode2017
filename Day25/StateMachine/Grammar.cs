using System.Linq;
using System.Text.RegularExpressions;
using Sprache;

namespace Day25.StateMachine
{
    internal static class Grammar
    {
        #region Common
        private static readonly Parser<string> NewlineParser =
            Parse.Regex(new Regex(@"\r?\n"));

        private static readonly Parser<StateMachineState> StateParser =
            Parse.Char('A').Return(StateMachineState.A)
                .Or(Parse.Char('B').Return(StateMachineState.B))
                .Or(Parse.Char('C').Return(StateMachineState.C))
                .Or(Parse.Char('D').Return(StateMachineState.D))
                .Or(Parse.Char('E').Return(StateMachineState.E))
                .Or(Parse.Char('F').Return(StateMachineState.F));
        #endregion

        #region Prelude
        private static readonly Parser<StateMachineState> PreludeStateParser =
            from text in Parse.String("Begin in state ")
            from state in StateParser
            from dot in Parse.Char('.')
            from newline in NewlineParser
            select state;

        private static readonly Parser<int> PreludeChecksumStepsParser =
            from text1 in Parse.String("Perform a diagnostic checksum after ")
            from number in Parse.Number
            from text2 in Parse.String(" steps.")
            from newline in NewlineParser
            select int.Parse(number);

        private static readonly Parser<Prelude> PreludeParser =
            from state in PreludeStateParser
            from steps in PreludeChecksumStepsParser
            select new Prelude(state, steps);
        #endregion

        private static readonly Parser<bool> WriteValueParser =
            from text in Parse.String("    - Write the value ")
            from value in Parse.Char('1').Return(true)
                .Or(Parse.Char('0').Return(false))
            from dot in Parse.Char('.')
            from newline in NewlineParser
            select value;

        private static readonly Parser<Direction> MoveDirectionParser =
            from text in Parse.String("    - Move one slot to the ")
            from direction in Parse.String("left").Return(Direction.Left)
                .Or(Parse.String("right").Return(Direction.Right))
            from dot in Parse.Char('.')
            from newline in NewlineParser
            select direction;

        private static readonly Parser<StateMachineState> ChangeStateParser =
            from text in Parse.String("    - Continue with state ")
            from state in StateParser
            from dot in Parse.Char('.')
            from newline in NewlineParser
            select state;

        private static readonly Parser<StateValueOperations> StateValueOperationsParser =
            from writeValue in WriteValueParser
            from moveDirection in MoveDirectionParser
            from changeState in ChangeStateParser
            select new StateValueOperations(writeValue, moveDirection, changeState);

        private static readonly Parser<StateOperation> StateOperationsParser =
            from text1 in Parse.String("In state ")
            from state in StateParser
            from colon in Parse.Char(':')
            from newline1 in NewlineParser
            from text2 in Parse.String("  If the current value is 0:")
            from newline2 in NewlineParser
            from stateValueOperationsZero in StateValueOperationsParser
            from text3 in Parse.String("  If the current value is 1:")
            from newline3 in NewlineParser
            from stateValueOperationsOne in StateValueOperationsParser
            select new StateOperation(state, stateValueOperationsZero, stateValueOperationsOne);

        public static readonly Parser<StateMachineProgram> StateMachineProgramParser =
            from prelude in PreludeParser
            from newline in NewlineParser
            from stateOperations in StateOperationsParser.DelimitedBy(NewlineParser)
            select new StateMachineProgram(prelude, stateOperations);
    }
}
