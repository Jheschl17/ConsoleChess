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
            _chessPieces.Add(new Rook(){Unicode = "Rook", Colour = ChessColour.Black, X = 0, Y = 0});
            // TODO add all chess pieces
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

            var canMove = piece.CanMove(xTo, yTo, _chessPieces);
            // If moving the chosen piece to the chosen field is not allowed according to the CanMove method implemented
            // in each ChessPiece subclass, return false.
            if (!canMove.canMove)
            {
                return (false, canMove.errorMessage);
            }
            
            // All checks succeeded, return true.
            return (true, null);
        }

        public void Draw()
        {
            DrawEmpty();
            DrawPieces();
        }

        /// <summary>
        /// Draw chess pieces from list _chessPieces by  overwriting characters in terminal using
        /// Console.SetCursorPosition.
        /// </summary>
        private static void DrawPieces()
        {
            throw new NotImplementedException();
        }

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