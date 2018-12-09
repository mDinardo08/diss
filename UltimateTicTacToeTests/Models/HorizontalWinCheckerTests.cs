using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class HorizontalWinCheckerTests
    {

        HorizontalWinChecker h;
        Mock<CompositeGame> MockException;
        Mock<CompositeGame> MockGame;
        [TestInitialize()]
        public void Startup()
        {
            h = new HorizontalWinChecker();
            MockException = new Mock<CompositeGame>(MockBehavior.Strict);
            MockGame = new Mock<CompositeGame>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
        }

        [TestMethod]
        public void WillSetVerticalWinCheckerAsSuccessorWhenConstructed()
        {
            h = new HorizontalWinChecker();
            Assert.IsTrue(h.successor is VerticleWinChecker);
        }

        [TestMethod]
        public void WillReturnAPointWhichLiesOnTheWinningLine()
        {
            var player = new Player();
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockGame.Object, MockGame.Object, MockGame.Object}
                });
            MockGame.Setup(x => x.getWinner()).Returns(player);
            Assert.AreEqual(player, h.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillReturnAPointOnWinningLineInTheSecondRow()
        { 
            var player = new Player();
            var MockException = new Mock<BoardGame>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object , MockException.Object},
                    new List<BoardGame>{MockGame.Object, MockGame.Object, MockGame.Object}
                });
            MockGame.Setup(x => x.getWinner()).Returns(player);
            Assert.AreEqual(player, h.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillReturnTheWinnerOfThreeGamesInTheThirdRow()
        {
            var player = new Player();
            var MockException = new Mock<BoardGame>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object , MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object , MockException.Object},
                    new List<BoardGame>{MockGame.Object, MockGame.Object, MockGame.Object}
                });
            MockGame.Setup(x => x.getWinner()).Returns(player);
            Assert.AreEqual(player, h.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillReturnNullIfThereAreNoWinnersHorizontally()
        {
            var player = new Player();
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockGame.Object, MockException.Object , MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object , MockGame.Object},
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object}
                });
            Assert.AreEqual(null, h.checkForWin(MockGame.Object));
        }
    }
}
