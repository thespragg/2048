using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048ConsoleGame
{
    public static class HelperMethods
    {
        public static List<EmptyCells> GetEmpty(this int[,] arr)
        {
            List<EmptyCells> EmptyCells = new List<EmptyCells>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr[i, j] == 0)
                    {
                        EmptyCells emptyCell = new EmptyCells()
                        {
                            XIndex = i,
                            YIndex = j
                        };
                        EmptyCells.Add(emptyCell);
                    }
                }
            }
            return EmptyCells;
        }

        public static ConsoleColor GetColour(int num)
        {
            if (num == 2) return ConsoleColor.Green;
            if (num == 4) return ConsoleColor.Magenta;
            if (num == 8) return ConsoleColor.Yellow;
            if (num == 16) return ConsoleColor.Red;
            if (num == 32) return ConsoleColor.Cyan;
            if (num == 64) return ConsoleColor.Blue;
            if (num == 128) return ConsoleColor.DarkGreen;
            if (num == 256) return ConsoleColor.DarkYellow;
            if (num == 512) return ConsoleColor.DarkBlue;
            if (num == 1024) return ConsoleColor.DarkMagenta;
            if (num == 2048) return ConsoleColor.DarkCyan;
            return ConsoleColor.Gray;
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
        }
    }

    class ColourOutput : IDisposable
    {
        public ColourOutput(ConsoleColor fg)
        {
            Console.ForegroundColor = fg;
        }

        public void Dispose()
        {
            Console.ResetColor();
        }
    }

    public struct EmptyCells
    {
        public int XIndex { get; set; }
        public int YIndex { get; set; }
    }


    
}
