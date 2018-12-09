using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.GameClasses;

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
                board = new List<List<Game>>()
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
                board = new List<List<Game>>
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
                board = new List<List<Game>>
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
                board = new List<List<Game>>
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
                board = new List<List<Game>>
                {
                    new List<Game>{MockGame.Object, MockException.Object , MockException.Object},
                    new List<Game>{MockException.Object, MockException.Object , MockGame.Object},
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object}
                }
            };
            game.getWinner();
        }

        [TestMethod]
        public void WillDetectWinnerInFirstColumn()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            var MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            Game game = new TicTacToe
            {
                board = new List<List<Game>>
                {
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object }
                }
            };
            Assert.AreEqual(player, game.getWinner());
        }

        [TestMethod]
        public void WillDetectWinnerInSecondColumn()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            var MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            Game game = new TicTacToe
            {
                board = new List<List<Game>>
                {
                    new List<Game>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<Game>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<Game>{MockException.Object, MockGame.Object, MockException.Object }
                }
            };
            Assert.AreEqual(player, game.getWinner());
        }

        [TestMethod]
        public void WillDetectWinnerInThirdColumn()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            var MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            Game game = new TicTacToe
            {
                board = new List<List<Game>>
                {
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object }
                }
            };
            Assert.AreEqual(player, game.getWinner());
        }

        [TestMethod]
        [ExpectedException(typeof(NoWinnerException))]
        public void WillDetectNoWinnerIfNoneExistsVeritcally()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            var MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            Game game = new TicTacToe
            {
                board = new List<List<Game>>
                {
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object }
                }
            };
            game.getWinner();
        }

        [TestMethod]
        public void WillDetectDiagonalWinsFromTopLeft()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            var MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            Game game = new TicTacToe
            {
                board = new List<List<Game>>
                {
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object},
                    new List<Game>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object}
                }
            };
            Assert.AreEqual(player, game.getWinner());
        }

        [TestMethod]
        public void WillDetectDiagonalWinsFromBottomLeft()
        {
            var player = new Player();
            var MockGame = new Mock<Game>(MockBehavior.Strict);
            MockGame.Setup(x => x.getWinner()).Returns(player);
            var MockException = new Mock<Game>(MockBehavior.Strict);
            MockException.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            Game game = new TicTacToe
            {
                board = new List<List<Game>>
                {
                    new List<Game>{MockException.Object, MockException.Object, MockGame.Object},
                    new List<Game>{MockException.Object, MockGame.Object, MockException.Object},
                    new List<Game>{MockGame.Object, MockException.Object, MockException.Object}
                }
            };
            Assert.AreEqual(player, game.getWinner());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WillThrowIndexOutOfRangeIfGettingInvalidPoint()
        {
            Game game = new TicTacToe
            {
                board = new List<List<Game>>()
            };
            game.getSector(new Point
            {
                X = -1,
                Y = -1
            });
        }

        [TestMethod]
        public void WillReturnTheSectorRequested()
        {
            Game temp = new TicTacToe();
            Game game = new TicTacToe
            {
                board = new List<List<Game>>
                {
                    new List<Game>{null, null, null},
                    new List<Game>{null, temp, null},
                    new List<Game>{null, null, null}
                }
            };
            Assert.AreEqual(temp, game.getSector(new Point{X = 1,Y = 1}));
        }

        [TestMethod]
        public void WillReturnTheBoardItIsGiven()
        {

        }
    }
}
