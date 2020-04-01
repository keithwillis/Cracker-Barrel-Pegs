using System;
namespace CrackerBarrelPegs.Board
{
    public class MoveHole
    {
        public MoveHole(Hole moveFrom, Hole moveTo, Hole jumpOver)
        {
            MoveTo = moveTo;
            MoveFrom = moveFrom;
            JumpOver = jumpOver;
        }

        public Hole MoveTo { get; }
        public Hole MoveFrom { get; }
        public Hole JumpOver { get; }

        public bool CanMove()
        {
            return JumpOver.HasPeg() && !MoveTo.HasPeg() && MoveFrom.HasPeg();
        }
    }
}
