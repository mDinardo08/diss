using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UltimateTicTacToe.Controllers;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Controllers
{
    [TestClass]
    public class GameControllerTests
    {
        GameController cont;
        Mock<IGameService> mockService;

        [TestMethod]
        public void WillPassGameAndMoveToLogicLayer()
        {
            BoardGame game = new TicTacToe(null);
            Move move = new Move();
            mockService = new Mock<IGameService>(MockBehavior.Strict);
            mockService.Setup(x => x.processMove(game)).Returns((BoardGameDTO)null);
            cont = new GameController(mockService.Object);
            cont.makeMove(game);
            Assert.IsTrue(mockService.Invocations.Count == 1);
        }
    }
}
