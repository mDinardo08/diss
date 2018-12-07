using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.GameClasses;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class TileTests
    {

        [TestMethod]
        public void WillRecordTheOwnerOfTheMoveAsTheWinnerOfTheTile()
        {
            Player p = new Player();
            Move move = new Move
            {
                owner = p
            };
            Tile t = new Tile();
            t.makeMove(move);
            Assert.AreEqual(p, t.getWinner());
        }

    }
}
