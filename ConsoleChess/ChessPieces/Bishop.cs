using System.Collections.Generic;

namespace ConsoleChess.ChessPieces
{
    public class Bishop : ChessPiece
    {
        public override (bool canMove, string errorMessage) CanMove(int xTo, int yTo, List<ChessPiece> pieces)
        {
            throw new System.NotImplementedException();
        }

        public override string Unicode()
        {
            return "♗";
        }
    }
}