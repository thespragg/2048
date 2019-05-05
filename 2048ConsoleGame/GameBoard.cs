using System;
using System.Collections.Generic;

namespace _2048ConsoleGame
{
    public class GameBoard
    {
        public int[,] BoardTiles { get; set; }
        public int CurrentScore { get; set; }

        public GameBoard()
        {
            this.BoardTiles = new int[4, 4];
            CurrentScore = 0;
        }

        public void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    using(new ColourOutput(HelperMethods.GetColour(BoardTiles[i, j])))
                    {
                        Console.Write(string.Format("{0,6}", BoardTiles[i, j]));
                    } 
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("\n Current score: " + CurrentScore);
            Console.WriteLine("Use the arrow keys to move the board");
        }

        public void PlayGame()
        {
            bool isPlaying = true;
            do
            {
                List<EmptyCells> emptyCells = BoardTiles.GetEmpty();

                if(emptyCells.Count != 0) {
                    InsertTwoOrFour(emptyCells);
                    DisplayBoard();
                }
                var input = Console.ReadKey();
                if(input.Key == ConsoleKey.UpArrow)
                {
                }
                if (input.Key == ConsoleKey.DownArrow)
                {
                }
                if (input.Key == ConsoleKey.LeftArrow)
                {
                }
                if (input.Key == ConsoleKey.RightArrow)
                {
                }
            } while (isPlaying);
        }

        public void InsertTwoOrFour(List<EmptyCells> emptyCells)
        {
            Random rng = new Random();
            int index = rng.Next(0, emptyCells.Count - 1);
            EmptyCells SelectedCell = emptyCells[index];
            int SelectIntToInsert = rng.Next(1, 10);
            if (SelectIntToInsert == 9) SelectIntToInsert = 4;
            else SelectIntToInsert = 2;
            BoardTiles[SelectedCell.XIndex, SelectedCell.YIndex] = SelectIntToInsert;
        }
    }
}

