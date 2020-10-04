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
            foreach (var i in Enumerable.Range(0, 7))
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
            foreach (var i in Enumerable.Range(0, 7))
            {
                _chessPieces.Add(new Pawn {Colour = ChessColour.Black, X = i, Y = 6});
            }
        }

        public (bool success, string errorMessage) Move(int xFrom, int yFrom, int xTo, int yTo)
        {
            var piece = (
                from pcs in _chessPieces
                where pcs.X == xFrom && pcs.Y == yFrom
                select pcs
            ).GetEnumerator().Current;

            // If no piece could be found on the given position (xFrom, yFrom), return false.
            if (piece == null)
            {
                return (false, "The given position is empty");
            }

            var (canMove, errorMessage) = piece.CanMove(xTo, yTo, _chessPieces);
            // If moving the chosen piece to the chosen field is not allowed according to the CanMove method implemented
            // in each ChessPiece subclass, return false.
            if (!canMove)
            {
                return (false, errorMessage);
            }

            // All checks succeeded, return true.
            return (true, null);
        }

        /// <summary>
        /// Print the current state of the board to the console. This method will overwrite the current state of the
        /// console by setting the cursor position.
        /// </summary>
        public void Draw()
        {
            DrawEmpty();
            drawPieces();
        }

        /// <summary>
        /// Draw chess pieces from list _chessPieces by  overwriting characters in terminal using
        /// Console.SetCursorPosition.
        /// </summary>
        private void drawPieces()
        {
            throw new NotImplementedException();
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