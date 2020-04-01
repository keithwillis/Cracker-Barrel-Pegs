using System;
using System.Collections.Generic;
using System.Linq;

namespace CrackerBarrelPegs.Board
{
    public class Peg
    {
        public Peg()
        {
        }

        public Hole Location { get; set; }

        public bool IsRemoved { get; set; }

        public List<string> PossibleMoves()
        {
            var moves = new List<string>();
            foreach (var item in Location.PossibleMoves())
            {
                moves.Add(item.MoveTo.Name);
            }
            return moves;
        }

        public void MoveTo(string Name)
        {
            var moveTo = Location.PossibleMoves().Where(h => h.MoveTo.Name == Name).FirstOrDefault();

            if (moveTo == null)
                throw new Exception("Invalid Move");

            Location.MovePeg(moveTo.MoveTo);
            moveTo.JumpOver.RemovePeg();
        }
    }
}
