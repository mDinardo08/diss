using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.MCTS;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class GoodDadTests
    {
        Player player;
        [TestInitialize()]
        public void Setup()
        {
            Mock<IDatabaseProvider> mockProvider = new Mock<IDatabaseProvider>();
            mockProvider.Setup(x => x.getUser(It.IsAny<int>())).Returns(new RatingDTO());
            player = new GoodDad(null, mockProvider.Object);
        }

        [TestMethod]
        public void WillCallForRewardFromNode()
        {
            Mock<INode> node = new Mock<INode>();
            node.Setup(x => x.getReward())
                .Returns(0)
                .Verifiable();
            node.Setup(x => x.getMove())
                .Returns(new Move());
            player.makeMove(null, new List<INode>
            {
                node.Object
            }, 0);
            node.Verify();
        }

        [TestMethod]
        public void WillCallAllNodesInList()
        {
            Mock<INode> node = new Mock<INode>();
            node.Setup(x => x.getReward())
                .Returns(0)
                .Verifiable();
            node.Setup(x => x.getMove())
                .Returns(new Move());
            player.makeMove(null, new List<INode>
            {
                node.Object, node.Object
            }, 0);
            node.Verify(x => x.getReward(), Times.Exactly(2));
        }

        [TestMethod]
        public void WillReturnANodeWhichRewardIsClosestTheDTOAverageValueIfLatestIsZero()
        {
            Mock<IDatabaseProvider> mockProvider = new Mock<IDatabaseProvider>();
            mockProvider.Setup(x => x.getUser(It.IsAny<int>()))
            .Returns(new RatingDTO { average = 1, latest = 0 });
            player = new GoodDad(null, mockProvider.Object);
            Mock<INode> node = new Mock<INode>();
            node.Setup(x => x.getReward())
                .Returns(0.9);
            node.Setup(x => x.getMove())
                .Returns(new Move());
            Mock<INode> badNode = new Mock<INode>();
            badNode.Setup(x => x.getReward()).Returns(-1);
            INode result = player.makeMove(null, new List<INode>
            {
                badNode.Object, node.Object
            }, 0);
            Assert.AreSame(node.Object, result);
        }

        [TestMethod]
        public void WillReturnANodeWhichRewardIs60PercentAverageAnd40PercentLatest()
        {
            Mock<IDatabaseProvider> mockProvider = new Mock<IDatabaseProvider>();
            mockProvider.Setup(x => x.getUser(It.IsAny<int>()))
            .Returns(new RatingDTO { average = 0.5, latest = 1 });
            player = new GoodDad(null, mockProvider.Object);
            Mock<INode> node = new Mock<INode>();
            node.Setup(x => x.getReward())
                .Returns(0.7);
            node.Setup(x => x.getMove())
                .Returns(new Move());
            Mock<INode> badNode = new Mock<INode>();
            badNode.Setup(x => x.getReward()).Returns(0.5);
            INode result = player.makeMove(null, new List<INode>
            {
                node.Object, badNode.Object
            }, 0);
            Assert.AreSame(node.Object, result);
        }
    }
}
