using System;
using System.Collections.Generic;
using System.Linq;

namespace CrackerBarrelPegs.Board
{
    public class Hole
    {
        private List<MoveHole> moves = new List<MoveHole>();

        public Hole()
        {

        }

        public Hole(List<MoveHole> moves)
        {
            this.moves = moves;
        }

        public string Name { get; set; }

        public Peg Peg { get; set; }

        public bool HasPeg()
        {
            return !(Peg == null);
        }

        internal void AddMoveHole(MoveHole hole)
        {
            moves.Add(hole);
        }

        public List<MoveHole> PossibleMoves()
        {
            return moves.Where(h => h.CanMove() == true).ToList();
        }

        public void RemovePeg()
        {
            Peg.IsRemoved = true;
            Peg.Location = null;
            ClearPeg();
        }

        public void MovePeg(Hole moveToHole)
        {
            Peg.Location = moveToHole;
            moveToHole.Peg = Peg;
            ClearPeg();
        }

        private void ClearPeg()
        {
            Peg = null;
        }
    }
}
