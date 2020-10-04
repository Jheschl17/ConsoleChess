using System.Collections.Generic;

namespace ConsoleChess.ChessPieces
{
    public class Rook : ChessPiece
    {
        public override (bool canMove, string errorMessage) CanMove(int xTo, int yTo, List<ChessPiece> pieces)
        {
            // TODO implement this method
            return (true, null);
        }

        public override string Unicode()
        {
            return "♖";
        }
    }
}