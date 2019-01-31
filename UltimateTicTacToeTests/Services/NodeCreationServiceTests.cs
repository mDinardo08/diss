using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Services
{
    [TestClass]
    public class NodeCreationServiceTests
    {
        NodeCreationService service;

        [TestInitialize()]
        public void Setup()
        {
            service = new NodeCreationService(null);
        }

        [TestMethod]
        public void WillReturnANode()
        {
            INode result = service.createNode(null);
            Assert.IsTrue(result is Node);
        }

        [TestMethod]
        public void WillPassBoardGameToNode()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Node result = service.createNode(mockGame.Object) as Node;
            Assert.IsTrue(result.subject == mockGame.Object);
        }

        [TestMethod]
        public void WillPassGameServiceToNode()
        {
            Mock<IGameService> mockService = new Mock<IGameService>();
            service = new NodeCreationService(mockService.Object);
            Node result = service.createNode(null) as Node;
            Assert.IsTrue(result.gameService == mockService.Object);

        }
    }
}
