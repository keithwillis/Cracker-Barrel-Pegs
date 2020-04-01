using System;
using System.Collections.Generic;
using System.Linq;

namespace CrackerBarrelPegs.Board
{
    public class PossibleMoves
    {
        public List<MoveHole> Moves { get; } = new List<MoveHole>();
        public Dictionary<string, Hole> Holes { get; } = new Dictionary<string, Hole>();

        public PossibleMoves()
        {
            //Create 15 holes and Map
            CreateHoles(15);
            GenerateMoveList();
            foreach (var hole in Holes)
            {
                AssociateMoveHoles(hole.Value);
            }
        }

        private void GenerateMoveList()
        {
            Moves.Add(CreateMoveHole("1", "4", "2"));
            Moves.Add(CreateMoveHole("1", "6", "3"));
            Moves.Add(CreateMoveHole("2", "7", "4"));
            Moves.Add(CreateMoveHole("2", "9", "5"));
            Moves.Add(CreateMoveHole("3", "8", "5"));
            Moves.Add(CreateMoveHole("3", "10", "6"));
            Moves.Add(CreateMoveHole("4", "1", "2"));
            Moves.Add(CreateMoveHole("4", "6", "5"));
            Moves.Add(CreateMoveHole("4", "11", "7"));
            Moves.Add(CreateMoveHole("4", "13", "8"));
            Moves.Add(CreateMoveHole("5", "12", "8"));
            Moves.Add(CreateMoveHole("5", "14", "9"));
            Moves.Add(CreateMoveHole("6", "1", "3"));
            Moves.Add(CreateMoveHole("6", "4", "5"));
            Moves.Add(CreateMoveHole("6", "13", "9"));
            Moves.Add(CreateMoveHole("6", "15", "10"));
            Moves.Add(CreateMoveHole("7", "2", "4"));
            Moves.Add(CreateMoveHole("7", "9", "8"));
            Moves.Add(CreateMoveHole("8", "3", "5"));
            Moves.Add(CreateMoveHole("8", "10", "9"));
            Moves.Add(CreateMoveHole("9", "2", "5"));
            Moves.Add(CreateMoveHole("9", "7", "8"));
            Moves.Add(CreateMoveHole("10", "3", "6"));
            Moves.Add(CreateMoveHole("10", "8", "9"));
            Moves.Add(CreateMoveHole("11", "4", "7"));
            Moves.Add(CreateMoveHole("11", "13", "12"));
            Moves.Add(CreateMoveHole("12", "5", "8"));
            Moves.Add(CreateMoveHole("12", "14", "13"));
            Moves.Add(CreateMoveHole("13", "4", "8"));
            Moves.Add(CreateMoveHole("13", "6", "9"));
            Moves.Add(CreateMoveHole("13", "11", "12"));
            Moves.Add(CreateMoveHole("13", "15", "14"));
            Moves.Add(CreateMoveHole("14", "5", "9"));
            Moves.Add(CreateMoveHole("14", "12", "13"));
            Moves.Add(CreateMoveHole("15", "6", "10"));
            Moves.Add(CreateMoveHole("15", "13", "14"));

        }

        private void AssociateMoveHoles(Hole hole)
        {
            foreach (var item in Moves.Where(mh => mh.MoveFrom.Name == hole.Name))
            {
                hole.AddMoveHole(item);
            }
        }

        private MoveHole CreateMoveHole(string moveFrom, string moveTo, string jumpHole)
        {
            return new MoveHole(Holes[moveFrom], Holes[moveTo], Holes[jumpHole]);
        }

        private void CreateHoles(int NumberOfHoles)
        {
            Holes.Clear();

            for (int h = 1; h <= NumberOfHoles; h++)
            {
                Hole hole = new Hole();
                hole.Name = h.ToString();
                Holes.Add(hole.Name, hole);
            }
        }

    }
}
