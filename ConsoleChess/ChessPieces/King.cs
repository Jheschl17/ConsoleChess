using System.Collections.Generic;

namespace ConsoleChess.ChessPieces
{
    public class King : ChessPiece
    {
        public override (bool canMove, string errorMessage) CanMove(int xTo, int yTo, List<ChessPiece> pieces)
        {
            // Check whether the given coordinates are different from the current coordinates
            if (!IsMoving(xTo, yTo))
            {
                return (false, "This is the initial position of the chosen chess piece");
            }

            // Check whether the given coordinates are inside the chess playing field
            if (MovesBeyondBoard(xTo, yTo))
            {
                return (false, "This position is beyond the chess playing field");
            }

            var xOk = xTo - 1 == X || xTo + 1 == X || xTo == X;
            var yOk = yTo - 1 == Y || yTo + 1 == Y || yTo == Y;
            if (!(xOk && yOk))
            {
                return (false, "The king can only move one square in either direction");
            }
            
            // Checks whether the given coordinates are blocked by a friendly piece
            if (MovesOntoFriendly(xTo, yTo, pieces))
            {
                return (false, "A friendly piece is at that position");
            }
            
            // All checks successful, can move is true
            return (true, null);
        }

        public override string Unicode() => "♔";
    }
}