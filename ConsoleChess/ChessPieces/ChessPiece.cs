using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace ConsoleChess.ChessPieces
{
    public abstract class ChessPiece
    {
        public ChessColour Colour { get; set; }

        /// <summary>
        /// The x (horizontal) position of this chess piece, where 0 is the very left. X rises as pieces go right on
        /// the board.
        /// </summary>
        public int X { get; set; }
        
        /// <summary>
        /// The y (vertical) position of this chess pieces, where 0 is the very bottom. Y rises as pieces go up the
        /// board.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Determines whether or not a piece can move to the given position. Also returns an error message in case it
        /// can not.
        /// </summary>
        /// <param name="xTo">
        /// The x (horizontal) position to move to, where 0 is the very left. X rises as pieces go
        /// right on the board.
        /// </param>
        /// <param name="yTo">
        /// The y (vertical) position to move to, where 0 is at the very bottom. Y rises as pieces go
        /// up the board.
        /// </param>
        /// <param name="pieces">The list of chess pieces currently on the board.</param>
        /// <returns>
        /// True if the piece can make the chosen move. In this case `canMove` is true and `errorMessage` null.
        /// Otherwise `canMove` is false and `errorMessage` contains a string message for the user, explaining why this
        /// particular move can not be performed.
        /// </returns>
        public abstract (bool canMove, string errorMessage) CanMove(int xTo, int yTo, List<ChessPiece> pieces);

        /// <summary>
        /// This method is overwritten by subclasses to return the unicode character according to the kind of chess
        /// piece.
        /// </summary>
        /// <returns>
        /// The unicode character corresponding to this chess piece. This would, for instance, return "♗" for a bishop.
        /// </returns>
        public abstract string Unicode();

        protected bool Equals(ChessPiece other)
        {
            return Colour == other.Colour && X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ChessPiece) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Colour;
                hashCode = (hashCode * 397) ^ X;
                hashCode = (hashCode * 397) ^ Y;
                return hashCode;
            }
        }

        /// <summary>
        /// Determines whether or not the current chess piece actually changes its position, were it to move to the
        /// given coordinates.
        /// </summary>
        /// <param name="xTo">
        /// The x (horizontal) position to move to, where 0 is the very left. X rises as pieces go right on the board
        /// </param>
        /// <param name="yTo">
        /// The y (vertical) position to move to, where 0 is the very bottom. Y rises as pieces go up on the board
        /// </param>
        /// <returns>Whether or not the chess piece changes position, were it to move to the given coordinates</returns>
        protected bool IsMoving(int xTo, int yTo)
        {
            return !(this.X == xTo && this.Y == yTo);
        }
    }
}