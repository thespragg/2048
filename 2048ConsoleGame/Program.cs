using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard gameBoard = new GameBoard();
            List<EmptyCells> emptyCells = gameBoard.BoardTiles.GetEmpty();
            gameBoard.InsertTwoOrFour(emptyCells);
            gameBoard.DisplayBoard();
            gameBoard.PlayGame();
        }
    }
}
