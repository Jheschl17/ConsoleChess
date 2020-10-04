using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleChess.ChessPieces;

namespace ConsoleChess
{
    public class ChessBoard
    {
        private readonly List<ChessPiece> _chessPieces = new List<ChessPiece>();

        public ChessBoard()
        {
            // -=-=-=- Initialize Chess Board -=-=-=-
            // White pieces
            _chessPieces.Add(new Rook {Colour = ChessColour.White, X = 0, Y = 0});
            _chessPieces.Add(new Knight {Colour = ChessColour.White, X = 1, Y = 0});
            _chessPieces.Add(new Bishop {Colour = ChessColour.White, X = 2, Y = 0});
            _chessPieces.Add(new Queen {Colour = ChessColour.White, X = 3, Y = 0});
            _chessPieces.Add(new King {Colour = ChessColour.White, X = 4, Y = 0});
            _chessPieces.Add(new Bishop {Colour = ChessColour.White, X = 5, Y = 0});
            _chessPieces.Add(new Knight {Colour = ChessColour.White, X = 6, Y = 0});
            _chessPieces.Add(new Rook {Colour = ChessColour.White, X = 7, Y = 0});
            // White pawns
            foreach (var i in Enumerable.Range(0, 8))
            {
                _chessPieces.Add(new Pawn {Colour = ChessColour.White, X = i, Y = 1});
            }
            
            // Black pieces
            _chessPieces.Add(new Rook {Colour = ChessColour.Black, X = 0, Y = 7});
            _chessPieces.Add(new Knight {Colour = ChessColour.Black, X = 1, Y = 7});
            _chessPieces.Add(new Bishop {Colour = ChessColour.Black, X = 2, Y = 7});
            _chessPieces.Add(new Queen {Colour = ChessColour.Black, X = 3, Y = 7});
            _chessPieces.Add(new King {Colour = ChessColour.Black, X = 4, Y = 7});
            _chessPieces.Add(new Bishop {Colour = ChessColour.Black, X = 5, Y = 7});
            _chessPieces.Add(new Knight {Colour = ChessColour.Black, X = 6, Y = 7});
            _chessPieces.Add(new Rook {Colour = ChessColour.Black, X = 7, Y = 7});
            // Black pawns
            foreach (var i in Enumerable.Range(0, 8))
            {
                _chessPieces.Add(new Pawn {Colour = ChessColour.Black, X = i, Y = 6});
            }
        }

        public (bool success, string errorMessage) Move(ChessColour chessColour, int xFrom, int yFrom, int xTo, int yTo)
        {
            var piece = (
                from pcs in _chessPieces
                where pcs.X == xFrom && pcs.Y == yFrom
                select pcs
            ).First();

            // If no piece could be found on the given position (xFrom, yFrom), return false.
            if (piece == null)
            {
                return (false, "This position is empty");
            }

            if (piece.Colour != chessColour)
            {
                return (false, "This piece is of the opposite colour");
            }

            var (canMove, errorMessage) = piece.CanMove(xTo, yTo, _chessPieces);
            // If moving the chosen piece to the chosen field is not allowed according to the CanMove method implemented
            // in each ChessPiece subclass, return false.
            if (!canMove)
            {
                return (false, errorMessage);
            }

            // All checks succeeded
            // If on exists, remove the chess piece currently at the position the moving piece moves to
            var take = (
                from pcs in _chessPieces
                where pcs.X == xTo && pcs.Y == yTo
                select pcs
            ).First();
            _chessPieces.Remove(take);

            return (true, null);
        }

        /// <summary>
        /// Print the current state of the board to the console. This method will overwrite the current state of the
        /// console.
        /// </summary>
        public void Draw()
        {
            Console.Clear();
            DrawEmpty();
            DrawPieces();
        }

        /// <summary>
        /// Draw chess pieces from list _chessPieces by  overwriting characters in terminal using
        /// Console.SetCursorPosition.
        /// </summary>
        private void DrawPieces()
        {
            var currentLeft = Console.CursorLeft;
            var currentTop = Console.CursorTop;
            var currentBackground = Console.BackgroundColor;
            var currentForeground = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Gray;
            foreach (var piece in _chessPieces)
            {
                var (cursorLeft, cursorTop) = ConsolePosition(piece.X, piece.Y);
                Console.SetCursorPosition(cursorLeft, cursorTop);
                // Set console foreground colour according the colour of the current piece
                switch (piece.Colour)
                {
                    case ChessColour.Black:
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case ChessColour.White:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                Console.Write(piece.Unicode());
            }
            
            // Reset cursor position, background colour and foreground colour to pre method invocation
            Console.SetCursorPosition(currentLeft, currentTop);
            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        /// <summary>
        /// Translates the board position of a chess piece (for instance x = 0, y = 2) to the cursor position required
        /// to overwrite the chess pieces representation on the console.
        /// </summary>
        /// <param name="x">
        /// The x (horizontal) position, where 0 is the very left. X rises as pieces go right on
        /// the board.
        /// </param>
        /// <param name="y">
        /// The y (horizontal) position, where 0 is the very left. X rises as pieces go right on
        /// the board.
        /// </param>
        /// <returns>The cursor position required to overwrite the chess piece the given position.</returns>
        private static (int cursorLeft, int cursorTop) ConsolePosition(int x, int y)
        {
            // The width in characters of a column
            const int columnSize = 5;
            // The amount of characters the end a column is off to the right from its center
            const int columnCenterOffset = 3;
            // The width in characters of the row indicator (1, 2, 3, 4, ... on the left side)
            const int rowIndicatorSize = 1;

            // The column position of the cursor
            var cursorLeft = rowIndicatorSize + (x + 1) * columnSize - columnCenterOffset;


            // The number of actual rows on the chess board, not number of rows displayed in the console
            const int rowAmount = 8;
            // The number of rows displayed on the console per actual chess board row
            const int consoleRowHeight = 2;
            
            // The row position of the cursor
            var cursorTop = (rowAmount - y) * consoleRowHeight - 1;

            return (cursorLeft, cursorTop);
        }
        
        /// <summary>
        /// Print an empty chess board to the console.
        /// </summary>
        private static void DrawEmpty()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            PrintTerminalLine();

            foreach (var i in Enumerable.Range(2, 7).Reverse())
            {
                Console.Write(i.ToString());
                PrintPieceLine();

                PrintPieceSeparatorLine();
            }

            Console.Write("1");
            PrintPieceLine();

            PrintTerminalLine();
            PrintAbcLine();
        }

        /// <summary>
        /// Prints the following using the correct colour scheme. The console colours will also be reset upon
        /// function completion.
        /// -----------------------------------------
        /// </summary>
        private static void PrintTerminalLine()
        {
            Console.Write(" ");

            var consoleBackground = Console.BackgroundColor;
            var consoleForeground = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("-----------------------------------------");

            Console.BackgroundColor = consoleBackground;
            Console.ForegroundColor = consoleForeground;
        }

        /// <summary>
        /// Prints the following using the correct colour scheme. The console colours will also be reset upon
        /// function completion.
        /// |    |    |    |    |    |    |    |    |
        /// </summary>
        private static void PrintPieceLine()
        {
            var consoleBackground = Console.BackgroundColor;
            var consoleForeground = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("|    |    |    |    |    |    |    |    |");

            Console.BackgroundColor = consoleBackground;
            Console.ForegroundColor = consoleForeground;
        }

        /// <summary>
        /// Prints the following using the correct colour scheme. The console colours will also be reset upon
        /// function completion.
        /// |----+----+----+----+----+----+----+----|
        /// </summary>
        private static void PrintPieceSeparatorLine()
        {
            Console.Write(" ");

            var consoleBackground = Console.BackgroundColor;
            var consoleForeground = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("|----+----+----+----+----+----+----+----|");

            Console.BackgroundColor = consoleBackground;
            Console.ForegroundColor = consoleForeground;
        }

        /// <summary>
        /// Prints the following using the correct colour scheme. The console colours will also be reset upon
        /// function completion.
        ///   a    b    c    d    e    f    g    h   
        /// </summary>
        private static void PrintAbcLine()
        {
            Console.Write(" ");

            var consoleBackground = Console.BackgroundColor;
            var consoleForeground = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("  a    b    c    d    e    f    g    h   ");

            Console.BackgroundColor = consoleBackground;
            Console.ForegroundColor = consoleForeground;
        }
    }
}