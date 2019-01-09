using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class TileTests
    {

        [TestMethod]
        public void WillRecordTheOwnerOfTheMoveAsTheWinnerOfTheTile()
        {
            Mock<Player> player = new Mock<Player>();
            Move move = new Move
            {
                owner = player.Object
            };
            Tile t = new Tile();
            t.makeMove(move);
            Assert.AreEqual(player.Object, t.getWinner());
        }

        [TestMethod]
        public void WillReturnAListWithAnEmptyMove()
        {
            Tile t = new Tile();
            Move result = t.getAvailableMoves()[0];
            Assert.IsTrue(result.next == null & result.possition == null && result.owner == null);
        }

        [TestMethod]
        public void WillReturnAnEmptyListIfTileIsAlreadyTaken()
        {
            Tile t = new Tile();
            t.owner = new Mock<Player>().Object;
            List<Move> result = t.getAvailableMoves();
            Assert.IsTrue(result.Count == 0);
        }

    }
}
