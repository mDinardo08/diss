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
    public class DiagonalWinCheckerTests
    {
        DiagonalWinChecker d;
        Mock<Game> MockException;
        [TestInitialize()]
        public void Setup()
        {
            d = new DiagonalWinChecker();
            MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
        }

        [TestMethod]
        public void WillDetectDiagonalWinsFromTopLeft()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getGame()).Returns(new List<List<Game>>
                {
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<Game>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object}
                }
            );
            Assert.AreEqual(player, d.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillDetectDiagonalWinsFromBottomLeft()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getGame()).Returns(new List<List<Game>>
                {
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<Game>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object}
                }
            );
            Assert.AreEqual(player, d.checkForWin(MockGame.Object));
        }
    }
}
