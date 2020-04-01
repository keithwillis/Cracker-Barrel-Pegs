using System;
using System.Collections.Generic;
using System.Linq;

namespace CrackerBarrelPegs.Board
{
    public class PegBoard
    {
        private PossibleMoves moves = new PossibleMoves();
        private int maxRows = 0;
        private List<Peg> pegs = new List<Peg>();
        private bool hasQuit = false;
        private LinkedList<Move> history = new LinkedList<Move>();

        public PegBoard(string startingHole, int rows)
        {
            maxRows = rows;
            foreach (var hole in moves.Holes)
            {
                if (hole.Key != startingHole)
                {
                    var p = new Peg();
                    p.Location = hole.Value;
                    hole.Value.Peg = p;
                    pegs.Add(p);
                }
            }
        }



        public void GenerateCurrentBoard()
        {
            Console.Clear();
            /*
                           1              l
                          2  3           l  l
                        4  5  6         l  l  l
                       7  8  9  10     l  l  l  l
                     11 12 13 14 15   l  l  o  l  l
            */

            //5 rows with double digits (15) - 3 spaces per number
            //number of rows = number of holes
            int currentNumber = 0;
            int endNumber = 0;
            int padWidth = 3;

            for (int row = 1; row <= maxRows; row++)
            {
                if (currentNumber != 0)
                    currentNumber = endNumber;
                else
                    currentNumber = 1;

                endNumber = currentNumber + row;
                int indentAmount = ((maxRows - (row - 1)) * padWidth) / 2;
                int spacingAmount = (maxRows - (row - 1)) * padWidth;

                WriteCurrentBoardLine(currentNumber, endNumber, padWidth, indentAmount, spacingAmount);
            }

            Console.WriteLine("Enter \"q\" to quit");

        }

        private void WriteCurrentBoardLine(int startNum, int endNum, int padWidth, int indentAmount, int spacingAmount)
        {
            string holeFill;

            Console.Write("".PadRight(indentAmount));

            for (int currentNumber = startNum; currentNumber < endNum; currentNumber++)
            {
                Console.Write(currentNumber.ToString().PadRight(padWidth));
            }

            Console.Write("".PadRight(spacingAmount));

            for (int currentNumber = startNum; currentNumber < endNum; currentNumber++)
            {

                if (moves.Holes[currentNumber.ToString()].HasPeg())
                    holeFill = "|";
                else
                    holeFill = "o";

                Console.Write(holeFill.PadRight(padWidth));

            }

            Console.WriteLine("");
        }

        private bool HasMoves()
        {

            var validMoves = moves.Holes.Where(h => h.Value.PossibleMoves().Count > 0).ToList();

            return validMoves.Count > 0 && !hasQuit;

        }

        private void RequestMove()
        {

            var validMoves = moves.Holes.Where(h => h.Value.PossibleMoves().Count > 0).ToList();

            Console.Write("Which Peg Would you like to move?");
            string movePeg = Console.ReadLine();
            if (movePeg.Trim().ToUpper() == "Q")
            {
                //quit
                hasQuit = true;
                return;
            }
            Console.Write("Which location would you like to put your peg ? ");
            string moveToPeg = Console.ReadLine();

            if (moveToPeg.Trim().ToUpper() == "Q")
            {
                //quit
                hasQuit = true;
                return;
            }
            else
            {
                //verify valid value
                //if invalid display clear console and display invalid message
                var moveHole = moves.Moves.Where(mh => mh.MoveFrom.Name == movePeg && mh.MoveTo.Name == moveToPeg).FirstOrDefault();
                if (moveHole != null)
                {
                    PerformMove(moveHole);
                    GenerateCurrentBoard();

                }
                else
                {
                    GenerateCurrentBoard();
                    Console.WriteLine("You must enter a valid value for Move From and Move To");
                }
            }

        }

        private void PerformMove(MoveHole moveHole)
        {
            moveHole.MoveFrom.MovePeg(moveHole.MoveTo);
            moveHole.JumpOver.RemovePeg();
        }

        private void UndoMove(MoveHole moveHole)
        {
            moveHole.MoveTo.MovePeg(moveHole.MoveFrom);
            //replace peg
            Peg peg = pegs.Where(p => p.IsRemoved == true).First();
            peg.IsRemoved = false;
            peg.Location = moveHole.JumpOver;
            moveHole.JumpOver.Peg = peg;

        }

        public void Play()
        {
            Console.Clear();
            GenerateCurrentBoard();

            while (HasMoves())
            {
                RequestMove();

                //if is valid move the perform move else show message
            }

        }

        public int PegsRemaining()
        {
            return pegs.Where(p => p.IsRemoved == false).Count();
        }

        public LinkedList<Move> Solve()
        {


            var validMove = GetValidMoves().FirstOrDefault();

            Move firstMove = new Move(null, validMove);
            PerformMove(firstMove);

            firstMove.childMoves = GetChildMoves(firstMove);

            return ProcessChildren(firstMove);
        }

        private List<Move> GetChildMoves(Move parent)
        {
            List<Move> moveList = new List<Move>();
            foreach (var moveHole in GetValidMoves())
            {
                moveList.Add(new Move(parent, moveHole));
            }
            return moveList;
        }

        public LinkedList<Move> ProcessChildren(Move parent)
        {
            var solution = new LinkedList<Move>();
            foreach (var childMove in parent.childMoves)
            {
                PerformMove(childMove);

                if (PegsRemaining() == 1)
                {

                    //success
                    solution.AddFirst(childMove);
                    Move prevParent = childMove.parent;
                    while (true)
                    {
                        if (prevParent != null)
                        {
                            solution.AddFirst(prevParent);
                            prevParent = prevParent.parent;
                        }
                        else
                        {
                            return solution;
                        }
                    }
                }
                else if (PegsRemaining() > 1 && HasMoves())
                {
                    childMove.childMoves = GetChildMoves(childMove);
                    var returnVal = ProcessChildren(childMove);
                    if (returnVal == null)
                        UndoMove(childMove);
                    else
                        return returnVal;
                }
                else
                {
                    //move to the next child as we didn't hit success
                    //undo the move
                    UndoMove(childMove);
                }
            }
            return null;
        }

        public List<MoveHole> GetValidMoves()
        {
            return moves.Moves.Where(h => h.CanMove() == true).ToList();
        }
    }
}