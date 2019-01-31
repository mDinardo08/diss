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
            service = new NodeCreationService();
        }

        [TestMethod]
        public void WillReturnANode()
        {
            INode result = service.createNode(null, 0);
            Assert.IsTrue(result is Node);
        }

        [TestMethod]
        public void WillPassBoardGameToNode()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Node result = service.createNode(mockGame.Object, 0) as Node;
            Assert.IsTrue(result.game == mockGame.Object);
        }
    }
}
