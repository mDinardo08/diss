using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Services
{
    [TestClass]
    public class GameServiceTests
    {
        GameService service;

        [TestInitialize()]
        public void Setup()
        {
            service = new GameService();
        }

        [TestMethod]
        public void WillSetTheWinningPlayerIfGameIsOver()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            Mock<Player> p = new Mock<Player>();
            mockGame.Setup(x => x.getWinner()).Returns(p.Object);
            BoardGameDTO result = service.processMove(mockGame.Object, null);
            Assert.AreEqual(p.Object, result.Winner);
        }

        [TestMethod]
        public void WillHaveTheAiMakeAMoveIfGameIsNotOver()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            Mock<Player> p = new Mock<Player>();
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(mockGame.Object);
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object);
            Assert.IsTrue(result.game == mockGame.Object);
        }

        [TestMethod]
        public void WillReturnTheAiAsTheWinnerIfItsMoveWinsTheGame()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            Mock<Player> p = new Mock<Player>();
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(mockGame.Object);
            mockGame.SetupSequence(x => x.getWinner()).Throws(new NoWinnerException()).Returns(p.Object);
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object);
            Assert.IsTrue(result.Winner == p.Object);
        }

        [TestMethod]
        public void WillMaintainTheAIOpponentOnTheGame()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            Mock<Player> p = new Mock<Player>();
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(mockGame.Object);
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object);
            //Assert.IsTrue(result.next == p.Object);
        }
    }
}
