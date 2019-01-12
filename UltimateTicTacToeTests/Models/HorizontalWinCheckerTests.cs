using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.WinCheck;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class HorizontalWinCheckerTests
    {

        HorizontalWinChecker h;
        Mock<BoardGame> MockException;
        Mock<BoardGame> MockGame;
        [TestInitialize()]
        public void Startup()
        {
            h = new HorizontalWinChecker();
            MockException = new Mock<BoardGame>(MockBehavior.Strict);
            MockGame = new Mock<BoardGame>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
        }

        [TestMethod]
        public void WillSetSuccessorToVerticalWinChecker()
        {
            h.setSuccessor();
            Assert.IsTrue(h.getSuccessor() is VerticleWinChecker);
        }

        [TestMethod]
        public void WillSetCheckMethodToCheckHorizontalWin()
        {
            h.setCheckFunction();
            Assert.AreEqual(h.check, h.checkHorizontalWinner);
        }

        [TestMethod]
        public void WillCallSuccessorIfNoWinDetected()
        {
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockException.Object }
                });
            Mock<IWinChecker> mock = new Mock<IWinChecker>(MockBehavior.Strict);
            mock.Setup(x => x.checkForWin(MockGame.Object)).Returns((Player)null);
            h.successor = mock.Object;
            h.check = (CompositeGame => new Point { X = -1, Y = -1 });
            h.checkForWin(MockGame.Object);
            Assert.IsTrue(mock.Invocations.Count == 1);
        }

        [TestMethod]
        public void WillReturnAPointWhichLiesOnTheWinningLine()
        {
            Mock<Player> player = new Mock<Player>();
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockGame.Object, MockGame.Object, MockGame.Object}
                });
            MockGame.Setup(x => x.getWinner()).Returns(player.Object);
            Assert.AreEqual(player.Object, h.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillReturnAPointOnWinningLineInTheSecondRow()
        {
            Mock<Player> player = new Mock<Player>();
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object , MockException.Object},
                    new List<BoardGame>{MockGame.Object, MockGame.Object, MockGame.Object}
                });
            MockGame.Setup(x => x.getWinner()).Returns(player.Object);
            Assert.AreEqual(player.Object, h.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillReturnTheWinnerOfThreeGamesInTheThirdRow()
        {
            Mock<Player> player = new Mock<Player>();
            var MockException = new Mock<BoardGame>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object , MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object , MockException.Object},
                    new List<BoardGame>{MockGame.Object, MockGame.Object, MockGame.Object}
                });
            MockGame.Setup(x => x.getWinner()).Returns(player.Object);
            Assert.AreEqual(player.Object, h.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillReturnNullIfThereAreNoWinnersHorizontally()
        {
            Mock<Player> player = new Mock<Player>();
            MockGame.Setup(x => x.getWinner()).Returns(player.Object);
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
