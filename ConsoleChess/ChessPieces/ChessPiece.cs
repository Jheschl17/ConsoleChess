using System.Collections.Generic;

namespace ConsoleChess.ChessPieces
{
    public abstract class ChessPiece
    {
        public string Unicode { get; protected set; }

        public ChessColour Colour { get; set; }

        public int X { get; set; }

        public int Y { get; set; }


        public abstract (bool canMove, string errorMessage) CanMove(int xTo, int yTo, List<ChessPiece> pieces);
    }
}