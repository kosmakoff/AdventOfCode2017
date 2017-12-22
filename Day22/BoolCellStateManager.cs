using System;

namespace Day22
{
    internal class BoolCellStateManager : CellStateManagerBase<bool>
    {
        public override Func<Direction, Direction> MutateDirection(bool state)
        {
            return IsInfected(state)
                ? (Func<Direction, Direction>) TurnRight
                : TurnLeft;
        }

        public override bool MutateState(bool state)
        {
            return !state;
        }

        public override bool IsInfected(bool state)
        {
            return state;
        }
    }
}