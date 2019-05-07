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
            while (true) {
                GameBoard gameBoard = new GameBoard();
                gameBoard.DisplayBoard();
                gameBoard.PlayGame();
                Console.ReadKey();
            }
            
        }
    }
}
