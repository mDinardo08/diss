using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class MoveTests
    {
        Move move;

        [TestInitialize()]
        public void Setup()
        {
            move = new Move();
        }

        [TestMethod]
        public void WillAsignTheOwnerAsThePlayerPassedIn()
        {
            Mock<Player> player = new Mock<Player>(MockBehavior.Loose);
            move.setOwner(0);
            Assert.AreEqual((PlayerColour)0, move.owner);
        }

        [TestMethod]
        public void WillPassTheOwnerToTheNestedMove()
        {
            Move nextMove = new Move();
            Mock<Player> mockPlayer = new Mock<Player>(MockBehavior.Loose);
            move.next = nextMove;
            move.setOwner(0);
            Assert.AreEqual(nextMove.owner, (PlayerColour)0);
        }
    }
}
