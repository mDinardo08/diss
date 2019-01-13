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
    public class VerticleWinCheckerTests
    {
        VerticleWinChecker v;
        Mock<BoardGame> MockException;
        Mock<BoardGame> MockGame;
        [TestInitialize()]
        public void Setup()
        {
            v = new VerticleWinChecker();
            MockException = new Mock<BoardGame>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            MockGame = new Mock<BoardGame>(MockBehavior.Strict);
        }

        [TestMethod]
        public void WillSetSuccessorToDiagonalWinChecker()
        {
            v.setSuccessor();
            Assert.IsTrue(v.getSuccessor() is DiagonalWinChecker);
        }

        [TestMethod]
        public void WillSetCheckFunctionToCheckVerticalWinner()
        {
            v.setCheckFunction();
            Assert.AreEqual(v.checkVerticalWinner, v.check);
        }

        [TestMethod]
        public void WillDetectWinnerInFirstColumn()
        {
            Mock<Player> player = new Mock<Player>();

            MockGame.Setup(x => x.getWinner()).Returns(player.Object);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object }
                });
            
            Point p = new Point { X = 0, Y = 0 };
            Assert.AreEqual(player.Object, v.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillDetectWinnerInSecondColumn()
        {
            Mock<Player> player = new Mock<Player>();

            MockGame.Setup(x => x.getWinner()).Returns(player.Object);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockGame.Object, MockException.Object }
                });

            Point p = new Point { X = 0, Y = 1 };
            Assert.AreEqual(player.Object, v.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillDetectWinnerInThirdColumn()
        {
            Mock<Player> player = new Mock<Player>();

            MockGame.Setup(x => x.getWinner()).Returns(player.Object);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object }
                });

            Point p = new Point { X = 0, Y = 2 };
            Assert.AreEqual(player.Object, v.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillReturnInvalidPointIfNoWinDetected()
        {

            Mock<Player> player = new Mock<Player>();

            MockGame.Setup(x => x.getWinner()).Returns(player.Object);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object }
                });
            Assert.AreEqual(null, v.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillCallSuccessorCheckForWinIfNoVerticalWinIsDetected()
        {
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockException.Object }
                });
            Mock<IWinChecker> mock = new Mock<IWinChecker>(MockBehavior.Strict);
            mock.Setup(x => x.checkForWin(MockGame.Object)).Returns((Player) null);
            v.successor = mock.Object;
            v.check = (CompositeGame => new Point { X=-1, Y=-1 });
            v.checkForWin(MockGame.Object);
            Assert.IsTrue(mock.Invocations.Count == 1);
        }
    }
}
