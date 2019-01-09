using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UltimateTicTacToe.Controllers;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Controllers
{
    [TestClass]
    public class GameControllerTests
    {
        GameController cont;
        Mock<IGameService> mockService;

        [TestMethod]
        public void WillPassGameAndAiToLogicLayer()
        {
            BoardGame game = new TicTacToe(null);
            Mock<Player> player = new Mock<Player>() ;
            mockService = new Mock<IGameService>(MockBehavior.Strict);
            mockService.Setup(x => x.processMove(game, player.Object)).Returns((BoardGameDTO)null);
            cont = new GameController(mockService.Object);
            cont.makeMove(new BoardGameDTO { game = game, next = player.Object });
            Assert.IsTrue(mockService.Invocations.Count == 1);
        }
    }
}
