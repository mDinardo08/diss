using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Services
{
    [TestClass]
    public class MonteCarloServiceTests
    {
        MonteCarloService service;

        [TestInitialize()]
        public void Setup()
        {
            service = new MonteCarloService(null);
        }

       [TestMethod]
       public void WillCallToCheckIfTheNodeIsALeaf()
        {
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.isLeaf())
                .Returns(true)
                .Verifiable();
            service.traverse(mockNode.Object);
            mockNode.Verify();
        }

        [TestMethod]
        public void WillReturnNodeIfItIsALeaf()
        {
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.isLeaf())
                .Returns(true);
            INode result = service.traverse(mockNode.Object);
            Assert.AreEqual(mockNode.Object, result);
        }

        [TestMethod]
        public void IfTheNodeIsNotALeafWillCallToGetChildNodes()
        {
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.SetupSequence(x => x.isLeaf())
                .Returns(false).Returns(true);
            mockNode.Setup(x => x.getChildren())
                .Returns(new List<INode>())
                .Verifiable();
            service.traverse(mockNode.Object);
            mockNode.Verify();
        }

        [TestMethod]
        public void WillCallForUCB1ValuesFromChildNodes()
        {
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.getUBC1())
                .Returns(0)
                .Verifiable();
            mockNode.SetupSequence(x => x.isLeaf())
                .Returns(false)
                .Returns(true);
            mockNode.Setup(x => x.getChildren())
                .Returns(new List<INode>
                {
                    mockNode.Object,mockNode.Object,mockNode.Object,mockNode.Object,
                })
                .Verifiable();
            service.traverse(mockNode.Object);
            mockNode.Verify(x => x.getUBC1(), Times.Exactly(4));
        }

        [TestMethod]
        public void WillCallIsLeafOnTheNodeWhichHasTheHighestUCB1Value()
        {
            Mock<INode> mockNode = new Mock<INode>();
            Mock<INode> mockNext = new Mock<INode>();
            mockNext.Setup(x => x.isLeaf())
                .Returns(true)
                .Verifiable();
            mockNext.Setup(x => x.getUBC1())
                .Returns(1);
            mockNode.Setup(x => x.getUBC1())
                .Returns(0);
            mockNode.Setup(x => x.isLeaf())
                .Returns(false);
            mockNode.Setup(x => x.getChildren())
                .Returns(new List<INode>
                {
                    mockNode.Object,mockNode.Object,mockNode.Object,mockNext.Object,
                })
                .Verifiable();
            service.traverse(mockNode.Object);
            mockNext.Verify();
        }
        
        [TestMethod]
        public void WillCallToCheckTheNumberOfVisitsOnTheNode()
        {
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.getVisits())
                .Returns(0)
                .Verifiable();
            service.expansion(mockNode.Object);
            mockNode.Verify();
        }

        [TestMethod]
        public void WillCallRolloutIfNodeHas0Visits()
        {
            Mock<INode> mockNode = new Mock<INode>(MockBehavior.Strict);
            mockNode.Setup(x => x.getVisits())
                .Returns(0);
            mockNode.Setup(x => x.rollOut())
                .Verifiable();
            service.expansion(mockNode.Object);
            mockNode.Verify();
        }

        [TestMethod]
        public void WillCallExpandOnNodeIfVisitsIsGreaterThanZero()
        {
            Mock<INode> mockNode = new Mock<INode>(MockBehavior.Strict);
            mockNode.Setup(x => x.getVisits())
                .Returns(1);
            mockNode.Setup(x => x.expand())
                .Verifiable();
            mockNode.Setup(x => x.getChildren())
                .Returns(new List<INode>
                {
                    new Mock<INode>().Object
                });
            service.expansion(mockNode.Object);
            mockNode.Verify();
        }

        [TestMethod]
        public void WillCallRolloutOnTheFirstChildNode()
        {
            Mock<INode> mockNode = new Mock<INode>(MockBehavior.Strict);
            Mock<INode> mockInnerNode = new Mock<INode>();
            mockInnerNode.Setup(x => x.rollOut())
                .Verifiable();
            mockNode.Setup(x => x.getVisits())
                .Returns(1);
            mockNode.Setup(x => x.expand());
            mockNode.Setup(x => x.getChildren())
                .Returns(new List<INode>
                {
                    mockInnerNode.Object
                });
            service.expansion(mockNode.Object);
            mockInnerNode.Verify();
        }

        [TestMethod]
        public void WillCallRollout3000Times()
        {
            DateTime startTime = DateTime.UtcNow;
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.isLeaf()).Returns(true);
            mockNode.Setup(x => x.getVisits()).Returns(0);
            mockNode.Setup(x => x.rollOut())
                .Verifiable();
            Mock<INodeCreationService> mockService = new Mock<INodeCreationService>();
            mockService.Setup(x => x.createNode(It.IsAny<BoardGame>(), It.IsAny<PlayerColour>()))
                .Returns(mockNode.Object);
            service = new MonteCarloService(mockService.Object);
            service.process(new Mock<BoardGame>().Object, 0);
            mockNode.Verify(x => x.rollOut(), Times.Exactly(3000));
        }

        [TestMethod]
        public void WillGetNodeCreationServiceToCreateRootNode()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.isLeaf()).Returns(true);
            mockNode.Setup(x => x.getVisits()).Returns(0);
            Mock<INodeCreationService> mockService = new Mock<INodeCreationService>();
            mockService.Setup(x => x.createNode(mockGame.Object, It.IsAny<PlayerColour>()))
                .Returns(mockNode.Object)
                .Verifiable();
            service = new MonteCarloService(mockService.Object);
            service.process(new Mock<BoardGame>().Object, 0);
            mockService.Verify();
        }
    }
}
