using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.WinCheck;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class DiagonalWinCheckerTests
    {
        DiagonalWinChecker d;
        Mock<BoardGame> MockException;
        Mock<BoardGame> MockGame;
        [TestInitialize()]
        public void Setup()
        {
            d = new DiagonalWinChecker();
            MockException = new Mock<BoardGame>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            MockGame = new Mock<BoardGame>(MockBehavior.Strict);
            MockGame.Setup(x => x.isWon()).Returns(true);
            MockException.Setup(x => x.isWon()).Returns(false);
        }

        [TestMethod]
        public void WillSetSuccessorToNull()
        {
            d.setSuccessor();
            Assert.IsNull(d.successor);
        }

        [TestMethod]
        public void WillSetCheckMethodToCheckDiagonalWins()
        {
            d.setCheckFunction();
            Assert.AreEqual(d.check, d.checkDiagonalWinner);
        }

        [TestMethod]
        public void WillReturnPointWithXisneg1IfNoWinFound()
        {
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockException.Object}
                }
            );
            Point result = d.checkDiagonalWinner(MockGame.Object);
            Assert.IsTrue(result.X == -1);
        }

        [TestMethod]
        public void WillDetectDiagonalWinsFromTopLeft()
        {
            Mock<Player> player = new Mock<Player>();
            MockGame.Setup(x => x.getWinner()).Returns(0);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object}
                }
            );
            Assert.AreEqual((PlayerColour)0, d.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillDetectDiagonalWinsFromBottomLeft()
        {

            Mock<Player> player = new Mock<Player>();

            MockGame.Setup(x => x.getWinner()).Returns(0);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<BoardGame>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object}
                }
            );
            Assert.AreEqual((PlayerColour)0, d.checkForWin(MockGame.Object));
        }
    }
}
