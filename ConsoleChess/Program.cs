using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Schema;
using ConsoleChess.ChessPieces;

namespace ConsoleChess
{
    internal class Program
    {
        private const string UserInputPattern = "[a-h][1-8]-[a-h][1-8]";
        
        public static void Main(string[] args)
        {
            var board = new ChessBoard();
            for (var turnNumber = 1;; turnNumber++)
            {
                Turn:
                    board.Draw();
                    
                    // Determine current player based on turn number
                    var currentPlayer = turnNumber % 2 == 0 ? ChessColour.Black : ChessColour.White;
                    // Prompt user for input
                    var promptString = $"{turnNumber}. {currentPlayer.ToString()}s turn : ";
                    Console.Write(promptString);
                    
                    // Read user input
                    var userInput = Console.ReadLine();
                    
                    // If user input is invalid, restart the turn procedure
                    if (!Regex.IsMatch(userInput, UserInputPattern))
                    {
                        ShowError("Invalid position, enter your move in the format coordinateFrom-coordinateTo");
                        goto Turn; // I'm sorry
                    }
                    
                    // Parse the user input
                    var (xFrom, yFrom, xTo, yTo) = ParseUserInput(userInput);
                    // Move based on the parsed user input
                    var (canMove, errorMessage) = board.Move(currentPlayer, xFrom, yFrom, xTo, yTo);

                    // If move is invalid, restart turn procedure
                    if (!canMove)
                    {
                        ShowError(errorMessage);
                        goto Turn;
                    }
                    
                    // No errors, finish turn
            }
        }

        /// <summary>
        /// Show given error message on the console and wait until user presses a key.
        /// </summary>
        /// <param name="errorMessage">The error message to display</param>
        private static void ShowError(string errorMessage)
        {
            
            var currentBackground = Console.BackgroundColor;
            var currentForeground = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.Write($"{errorMessage} (Press any key to continue)");
            Console.Read();
            
            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        /// <summary>
        /// Parses a user input string. The input must match the regex [a-h][1-8]-[a-h][1-8]
        /// </summary>
        /// <param name="userInput">The user input as a string</param>
        /// <returns>The positions to and from, parsed from the user input</returns>
        private static (int xFrom, int yFrom, int xTo, int yTo) ParseUserInput(string userInput)
        {
            var xFrom = userInput[0] - 97;
            var yFrom = int.Parse(userInput[1].ToString()) - 1;
            var xTo = userInput[3] - 97;
            var yTo = int.Parse(userInput[4].ToString()) - 1;

            return (xFrom, yFrom, xTo, yTo);
        }
    }
}