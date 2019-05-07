using System;
using System.Collections.Generic;

namespace _2048ConsoleGame
{
    public class GameBoard
    {
        public int[,] BoardTiles;
        public bool wasMoveValid;

        public GameBoard()
        {
            this.BoardTiles = new int[4, 4];
        }

        public void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    using (new ColourOutput(HelperMethods.GetColour(BoardTiles[i, j])))
                    {
                        Console.Write(string.Format("{0,6}", BoardTiles[i, j]));
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("Use the arrow keys to move the board");
        }

        public void PlayGame()
        {
            bool isPlaying = true;
            wasMoveValid = true;
            BoardMovement moveBoard = new BoardMovement();
            do
            {
                List<EmptyCells> emptyCells = BoardTiles.GetEmpty();

                if (emptyCells.Count != 0 && wasMoveValid)
                {
                    moveBoard.CurrentScore += InsertTwoOrFour(emptyCells);
                    DisplayBoard();
                    Console.WriteLine("\n Current score: " + moveBoard.CurrentScore);
                }
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.UpArrow)
                {
                    wasMoveValid = moveBoard.MoveBoard(HelperMethods.Direction.Up, ref BoardTiles);
                }
                if (input.Key == ConsoleKey.DownArrow)
                {
                    wasMoveValid = moveBoard.MoveBoard(HelperMethods.Direction.Down, ref BoardTiles);
                }
                if (input.Key == ConsoleKey.LeftArrow)
                {
                    wasMoveValid = moveBoard.MoveBoard(HelperMethods.Direction.Left, ref BoardTiles);
                }
                if (input.Key == ConsoleKey.RightArrow)
                {
                    wasMoveValid = moveBoard.MoveBoard(HelperMethods.Direction.Right, ref BoardTiles);
                }
            } while (isPlaying);

        }

        public int InsertTwoOrFour(List<EmptyCells> emptyCells)
        {
            Random rng = new Random();
            int index = rng.Next(0, emptyCells.Count - 1);
            EmptyCells SelectedCell = emptyCells[index];
            int SelectIntToInsert = rng.Next(1, 10);
            if (SelectIntToInsert == 9) SelectIntToInsert = 4;
            else SelectIntToInsert = 2;
            BoardTiles[SelectedCell.XIndex, SelectedCell.YIndex] = SelectIntToInsert;
            return SelectIntToInsert;
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
        }
    }
}

