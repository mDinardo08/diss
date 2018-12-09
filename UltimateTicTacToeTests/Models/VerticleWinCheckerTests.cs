using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.GameClasses;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class VerticleWinCheckerTests
    {
        VerticleWinChecker v;
        Mock<Game> MockException;
        [TestInitialize()]
        public void Setup()
        {
            v = new VerticleWinChecker();
            MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
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
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getGame()).Returns(new List<List<Game>>
                {
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object }
                });
            
            Point p = new Point { X = 0, Y = 0 };
            Assert.AreEqual(player, v.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillDetectWinnerInSecondColumn()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getGame()).Returns(new List<List<Game>>
                {
                    new List<Game>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<Game>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<Game>{MockException.Object, MockGame.Object, MockException.Object }
                });

            Point p = new Point { X = 0, Y = 1 };
            Assert.AreEqual(player, v.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillDetectWinnerInThirdColumn()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getGame()).Returns(new List<List<Game>>
                {
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object }
                });

            Point p = new Point { X = 0, Y = 2 };
            Assert.AreEqual(player, v.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillReturnInvalidPointIfNoWinDetected()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getGame()).Returns(new List<List<Game>>
                {
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object }
                });
            Assert.AreEqual(null, v.checkForWin(MockGame.Object));
        }
    }
}
