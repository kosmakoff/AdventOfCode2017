using System;

namespace Day22
{
    internal class CellStateManager : CellStateManagerBase<CellState>
    {
        public override Func<Direction, Direction> MutateDirection(CellState state)
        {
            switch (state)
            {
                case CellState.Clean:
                    return TurnLeft;
                case CellState.Weakened:
                    return NoTurn;
                case CellState.Infected:
                    return TurnRight;
                case CellState.Flagged:
                    return TurnBack;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public override CellState MutateState(CellState state)
        {
            switch (state)
            {
                case CellState.Clean:
                    return CellState.Weakened;
                case CellState.Weakened:
                    return CellState.Infected;
                case CellState.Infected:
                    return CellState.Flagged;
                case CellState.Flagged:
                    return CellState.Clean;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public override bool IsInfected(CellState state)
        {
            return state == CellState.Infected;
        }
    }
}
