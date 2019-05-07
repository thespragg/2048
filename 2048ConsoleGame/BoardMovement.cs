using System;

namespace _2048ConsoleGame
{
    public class BoardMovement
    {
        public int CurrentScore { get; set; }
        public BoardMovement()
        {
            this.CurrentScore = 0;
        }

        public bool MoveBoard(HelperMethods.Direction direction, ref int[,] BoardTiles)
        {
            int ScoreIncrease = 0;
            //Assert if vertical or horizontal
            bool isVertical = direction == HelperMethods.Direction.Up || direction == HelperMethods.Direction.Down;
            //Assert if +ve or -ve
            bool isPositive = direction == HelperMethods.Direction.Up || direction == HelperMethods.Direction.Left;

            //Get first index to check (2048 collapses from border outwards)
            int StartIndex = isPositive ? 0 : 3;
            //Flip end index depending on start
            int EndIndex = StartIndex == 3 ? 0 : 3;
            //Is loop increasing or decreasing
            int IncreaseOrDecrease = isPositive ? 1 : -1;

            bool wasMoveValid = false;

            Func<int[,], int, int, int> GetValueInCell = isVertical ? new Func<int[,], int, int, int>((i, j, k) => i[k, j]) : new Func<int[,], int, int, int>((i, j, k) => i[j, k]);
            Action<int[,], int, int, int> SetValueInNewCell = isVertical ? new Action<int[,], int, int, int>((i, j, k, l) => i[k, j] = l) : new Action<int[,], int, int, int>((i, j, k, l) => i[j, k] = l);
            Action<int[,], int, int> ResetCell = isVertical ? new Action<int[,], int, int>((i, j, k) => i[k, j] = 0) : new Action<int[,], int, int>((i, j, k) => i[j, k] = 0);

            //Asserts whether the loop is valid, even if its decreasing 
            bool continueLoop(int i) => Math.Min(StartIndex, EndIndex) <= i && Math.Max(StartIndex, EndIndex) >= i;
            //Used to increment index in the correct direction
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
                        ScoreIncrease += CurrentCellValue;
                        wasMoveValid = true;
                    }
                    else
                    {
                        incrementIndex(ref tempJ);
                        SetValueInNewCell(BoardTiles, i, tempJ, CurrentCellValue);
                        if (tempJ != j)
                        {
                            ResetCell(BoardTiles, i, j);
                            wasMoveValid = true;
                        } 
                    }
                }
            }
            return wasMoveValid;
        }
    }
}
