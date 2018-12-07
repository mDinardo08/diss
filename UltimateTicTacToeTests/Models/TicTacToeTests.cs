using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class TicTacToeTests
    {

        [TestMethod]
        [ExpectedException(typeof(NoWinnerException))]
        public void WillThrowNoWinnerExceptionOnNewBoard()
        {
            Game game = new TicTacToe
            {
                game = new List<List<Game>>()
            };
            game.getWinner();
        }

        [TestMethod]
        public void WillReturnTheWinnerOfThreeGamesInTheFirstRow()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            Game game = new TicTacToe
            {
                game = new List<List<Game>>
                {
                    new List<Game>{MockGame.Object, MockGame.Object, MockGame.Object}
                }
            };
            Assert.AreEqual(player, game.getWinner());
        }

        [TestMethod]
        public void WillReturnTheWinnerOfThreeGamesInTheSecondRow()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            var MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            Game game = new TicTacToe
            {
                game = new List<List<Game>>
                {
                    new List<Game>{MockException.Object, MockException.Object , MockException.Object},
                    new List<Game>{MockGame.Object, MockGame.Object, MockGame.Object}
                }
            };
            Assert.AreEqual(player, game.getWinner());
        }

        [TestMethod]
        public void WillReturnTheWinnerOfThreeGamesInTheThirdRow()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            var MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            Game game = new TicTacToe
            {
                game = new List<List<Game>>
                {
                    new List<Game>{MockException.Object, MockException.Object , MockException.Object},
                    new List<Game>{MockException.Object, MockException.Object , MockException.Object},
                    new List<Game>{MockGame.Object, MockGame.Object, MockGame.Object}
                }
            };
            Assert.AreEqual(player, game.getWinner());
        }

        [TestMethod]
        [ExpectedException(typeof(NoWinnerException))]
        public void WillThrowAnExceptionIfTherePointsAreTakenButNotEntireRow()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            var MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            Game game = new TicTacToe
            {
                game = new List<List<Game>>
                {
                    new List<Game>{MockGame.Object, MockException.Object , MockException.Object},
                    new List<Game>{MockException.Object, MockException.Object , MockGame.Object},
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object}
                }
            };
            game.getWinner();
        }
    }
}
