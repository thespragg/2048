using System;
using System.Collections.Generic;

namespace _2048ConsoleGame
{
    public class GameBoard
    {
        public int[,] BoardTiles;
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
            CurrentScore += SelectIntToInsert;
        }

        public void MoveBoard(Direction direction)
        {
            Console.Clear();
            DisplayBoard();
            //Assert if vertical or horizontal
            bool isVertical = direction == Direction.Up || direction == Direction.Down;
            //Assert if +ve or -ve
            bool isPositive = direction == Direction.Up || direction == Direction.Left;

            //Get first index to check (2048 collapses from border outwards)
            int StartIndex = isPositive ? 0 : 3;
            int EndIndex = StartIndex == 3 ? 0 : 3;
            int IncreaseOrDecrease = isPositive ? 1 : -1;

            //Function to find the value thats in the cell to be moved into
            Func<int[,], int, int, int> GetValueInCell = isVertical ? new Func<int[,], int, int, int>((i, j, k) => i[k, j]) : new Func<int[,], int, int, int>((i, j, k) => i[j, k]);
            //Function to set the value in the new cell
            Action<int[,], int, int, int> SetValueInNewCell = isVertical ? new Action<int[,], int, int, int>((i, j, k, l) => i[k, j] = l) : new Action<int[,], int, int, int>((i, j, k, l) => i[j, k] = l);
            Func<int[,], int, int, bool> DoValuesMatch = isVertical ? new Func<int[,], int, int, bool>((i, j, k) => i[k, j] == i[k, j]) : new Func<int[,], int, int, bool>((i, j, k) => i[j, k] == i[j, k]);
            Action<int[,], int, int> ResetCell = isVertical ? new Action<int[,], int, int>((i, j, k) => i[k, j] = 0) : new Action<int[,], int, int>((i, j, k) => i[j, k] = 0);

            //Allows loop to continue whethers it's looping forward or back
            bool continueLoop(int i) => Math.Min(StartIndex, EndIndex) <= i && Math.Max(StartIndex, EndIndex) >= i;
            int incrementIndex(ref int i) => isPositive ? i += 1 : i -= 1;

            int NextTileIndex(int i) => isPositive ? i -= 1 : i += 1;

            for (int i = 0; i < 4; i++)
            {
                for (int j = StartIndex; continueLoop(j); incrementIndex(ref j))
                {
                    int CurrentCellValue = GetValueInCell(BoardTiles, i, j);
                    if (CurrentCellValue == 0)
                    {
                        continue;
                    }

                    //Sets index to next tile
                    int tempJ = j;
                    //Continue if cell is already add a border
                    if (!continueLoop(NextTileIndex(tempJ)))
                    {
                        continue;
                    }

                    //Loops while the index is still valid and the next cell contains a 0
                    do
                    {
                        tempJ = NextTileIndex(tempJ);
                    } while (continueLoop(tempJ) && GetValueInCell(BoardTiles, i, tempJ) == 0);

                    if (continueLoop(tempJ) && CurrentCellValue == GetValueInCell(BoardTiles, i, tempJ))
                    {
                        SetValueInNewCell(BoardTiles, i, tempJ, CurrentCellValue * 2);
                        ResetCell(BoardTiles, i, j);
                        CurrentScore += CurrentCellValue;
                    }
                    else { 
                        incrementIndex(ref tempJ);
                        SetValueInNewCell(BoardTiles, i, tempJ, CurrentCellValue);
                        if(tempJ != j)
                        {
                            ResetCell(BoardTiles, i, j);
                        }
                    }
                }
            }
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

