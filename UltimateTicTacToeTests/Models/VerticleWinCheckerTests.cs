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
    public class VerticleWinCheckerTests
    {
        VerticleWinChecker v;
        Mock<CompositeGame> MockException;
        Mock<CompositeGame> MockGame;
        [TestInitialize()]
        public void Setup()
        {
            v = new VerticleWinChecker();
            MockException = new Mock<CompositeGame>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            MockGame = new Mock<CompositeGame>(MockBehavior.Strict);
        }


        [TestMethod]
        public void WillSetVerticalWinCheckerAsSuccessorWhenConstructed()
        {
            v = new VerticleWinChecker();
            Assert.IsTrue(v.successor is DiagonalWinChecker);
        }

        [TestMethod]
        public void WillDetectWinnerInFirstColumn()
        {
            var player = new Player();
            
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object }
                });
            
            Point p = new Point { X = 0, Y = 0 };
            Assert.AreEqual(player, v.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillDetectWinnerInSecondColumn()
        {
            var player = new Player();
            
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockGame.Object, MockException.Object }
                });

            Point p = new Point { X = 0, Y = 1 };
            Assert.AreEqual(player, v.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillDetectWinnerInThirdColumn()
        {
            var player = new Player();
            
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object }
                });

            Point p = new Point { X = 0, Y = 2 };
            Assert.AreEqual(player, v.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillReturnInvalidPointIfNoWinDetected()
        {
            var player = new Player();
            
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object }
                });
            Assert.AreEqual(null, v.checkForWin(MockGame.Object));
        }
    }
}
