using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.Game;

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

        [TestMethod]
        public void WillReturnAListWithANullMove()
        {
            Tile t = new Tile();
            List<Move> result = t.getAvailableMoves();
            Assert.IsNull(result[0]);
        }

        [TestMethod]
        public void WillReturnNullIfTileIsAlreadyTaken()
        {
            Tile t = new Tile();
            t.owner = new Player();
            List<Move> result = t.getAvailableMoves();
            Assert.IsNull(result);
        }

    }
}
