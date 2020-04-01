using System;
using System.Collections.Generic;

namespace CrackerBarrelPegs.Board
{
    public class Move : MoveHole
    {
        public Move parent { get; } = null;

        public List<Move> childMoves { get; set; } = new List<Move>();

        public Move(Move parent, MoveHole moveHole) : base(moveHole.MoveFrom, moveHole.MoveTo, moveHole.JumpOver)
        {
            this.parent = parent;
        }

    }
}
