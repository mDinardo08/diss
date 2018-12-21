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
            BoardGameDTO result = service.processMove(mockGame.Object);
            Assert.AreEqual(p.Object, result.Winner);
        }

    }
}
