using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _2048ConsoleGame;

namespace _2048ConsoleGameTests
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        public void IsSelectedCellEmpty_True()
        {
            var gameBoard = new GameBoard();
            Assert.IsTrue(gameBoard.isCellEmpty(0,0));
        }

        [TestMethod]
        public void IsSelectedCellEmpty_False()
        {
            var gameBoard = new GameBoard();
            gameBoard.BoardTiles[0][0] = '2';
            Assert.IsFalse(gameBoard.isCellEmpty(0, 0));
        }
    }
}
