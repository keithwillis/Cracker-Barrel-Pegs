using System;
using CrackerBarrelPegs.Board;

namespace CrackerBarrelPegs
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var board = new PegBoard("1", 5);

            //board.Play();

            foreach (var item in board.Solve())
            {
                Console.WriteLine(string.Format("From: {0} To: {1} Removed Peg: {2}", item.MoveFrom.Name, item.MoveTo.Name, item.JumpOver.Name));
            }


            Console.ReadLine();
        }


    }
}
