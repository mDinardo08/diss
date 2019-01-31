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
                owner = 0
            };
            Tile t = new Tile();
            t.makeMove(move);
            Assert.AreEqual((PlayerColour)0, t.getWinner());
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
            t.owner = 0;
            List<Move> result = t.getAvailableMoves();
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void WillReturnAObjectWhichIsNotARefernceToIt()
        {
            Tile tile = new Tile();
            Assert.AreNotSame(tile, tile.Clone());
        }

        [TestMethod]
        public void WillReturnANewOwner()
        {
            Tile tile = new Tile();
            tile.owner = (PlayerColour)100;
            Assert.AreNotSame(tile.owner, (tile.Clone() as Tile).owner);
        }
    }
}
