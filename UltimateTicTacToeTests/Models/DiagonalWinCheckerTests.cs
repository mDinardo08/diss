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
    public class DiagonalWinCheckerTests
    {
        DiagonalWinChecker d;
        Mock<CompositeGame> MockException;
        Mock<CompositeGame> MockGame;
        [TestInitialize()]
        public void Setup()
        {
            d = new DiagonalWinChecker();
            MockException = new Mock<CompositeGame>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            MockGame = new Mock<CompositeGame>(MockBehavior.Strict);
        }

        [TestMethod]
        public void WillDetectDiagonalWinsFromTopLeft()
        {
            var player = new Player();
            
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object}
                }
            );
            Assert.AreEqual(player, d.checkForWin(MockGame.Object));
        }

        [TestMethod]
        public void WillDetectDiagonalWinsFromBottomLeft()
        {
            var player = new Player();
            
            MockGame.Setup(x => x.getWinner()).Returns(player);
            MockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>
                {
                    new List<BoardGame>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<BoardGame>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<BoardGame>{MockGame.Object, MockException.Object, MockException.Object}
                }
            );
            Assert.AreEqual(player, d.checkForWin(MockGame.Object));
        }
    }
}
