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
            BoardTiles[0, 0] = 1;
            BoardTiles[0, 1] = 2;
            BoardTiles[0, 2] = 3;
            BoardTiles[0, 3] = 4;
            BoardTiles[1, 0] = 5;
            BoardTiles[1, 1] = 6;
            BoardTiles[1, 2] = 7;
            BoardTiles[1, 3] = 8;
            BoardTiles[2, 0] = 9;
            BoardTiles[2, 1] = 10;
            BoardTiles[2, 2] = 11;
            BoardTiles[2, 3] = 12;
            BoardTiles[3, 0] = 13;
            BoardTiles[3, 1] = 14;
            BoardTiles[3, 2] = 15;
            BoardTiles[3, 3] = 16;
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
            Console.WriteLine("\n Current score: " + CurrentScore);
            Console.WriteLine("Use the arrow keys to move the board");
        }

        public void PlayGame()
        {
            bool isPlaying = true;
            do
            {
                List<EmptyCells> emptyCells = BoardTiles.GetEmpty();

                if (emptyCells.Count != 0)
                {
                    InsertTwoOrFour(emptyCells);
                    DisplayBoard();
                }
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.UpArrow)
                {
                    MoveBoard(Direction.Up);
                }
                if (input.Key == ConsoleKey.DownArrow)
                {
                    MoveBoard(Direction.Down);
                }
                if (input.Key == ConsoleKey.LeftArrow)
                {
                    MoveBoard(Direction.Left);
                }
                if (input.Key == ConsoleKey.RightArrow)
                {
                    MoveBoard(Direction.Right);
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

        public void MoveBoard(Direction direction)
        {
            //Assert if vertical or horizontal
            bool isVertical = direction == Direction.Up || direction == Direction.Down;
            //Assert if +ve or -ve
            bool isPositive = direction == Direction.Up || direction == Direction.Left;

            //Get first index to check (2048 collapses from border outwards)
            int StartIndex = isPositive ? 0 : 3;
            int EndIndex = StartIndex == 3 ? 0 : 3;
            int IncreaseOrDecrease = isPositive ? 1 : -1;

            //Function to find the value thats in the cell to be moved into
            Func<int[,], int, int, int> GetValueInNewCell = isVertical ? new Func<int[,], int, int, int>((i, j, k) => i[k, j]) : new Func<int[,], int, int, int>((i, j, k) => i[k, j]);
            //Function to set the value in the new cell
            Action<int[,], int, int, int> SetValueInNewCell = isVertical ? new Action<int[,], int, int, int>((i, j, k, l) => i[k, j] = l) : new Action<int[,], int, int, int>((i, j, k, l) => i[j, k] = l);

            Func<int[,], int, int, bool> DoValuesMatch = isVertical ? new Func<int[,], int, int, bool>((i, j, k) => i[k, j] == i[k - IncreaseOrDecrease, j]) : new Func<int[,], int, int, bool>((i, j, k) => i[j, k] == i[j, k - IncreaseOrDecrease]);

            for (int i = 0; i < 4; i++)
            {
                for (int j = StartIndex; j >= EndIndex; j--)
                {
                    Console.WriteLine("Order of movement");
                    Console.WriteLine(BoardTiles[i,j]);
                }
            }
            //For each cell in row or column, starting at game edge
            //if item to move == space to take, multiply them
            //replace moved cell with 0, check to see if item can move again
            //If cant move, move to next cell to move and repeat
            //Move to next row or column and start iteration again
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

