using System;
using System.Linq;

namespace _2048ConsoleGame
{
    public class GameBoard
    {
        public char[][] BoardTiles { get; set; }
        public int CurrentScore { get; set; }

        public GameBoard()
        {
            BoardTiles = new char[][] { new char[] { '-', '-', '-', '-' }, new char[] { '-', '-', '-', '-' }, new char[] { '-', '-', '-', '-' }, new char[] { '-', '-', '-', '-' } };
            CurrentScore = 0;
        }

        public void DisplayBoard()
        {
            Console.Clear();
            foreach (var row in BoardTiles)
            {
               Console.WriteLine(string.Join("|",row));
            }
            Console.WriteLine("\n Current score: " + CurrentScore);
        }

        public bool isCellEmpty(int row, int column)
        {
            return (BoardTiles[row][column] == '-');
        }
    }
}
