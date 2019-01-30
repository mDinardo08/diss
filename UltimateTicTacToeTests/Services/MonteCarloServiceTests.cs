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
    public class MonteCarloServiceTests
    {
        MonteCarloService service;

        [TestInitialize()]
        public void Setup()
        {
            service = new MonteCarloService();
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
            service.rollout(mockNode.Object);
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
            service.rollout(mockNode.Object);
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
            service.rollout(mockNode.Object);
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
            service.rollout(mockNode.Object);
            mockInnerNode.Verify();
        }

        [TestMethod]
        public void WillRunForOverOneAndAHalfSeconds()
        {
            DateTime startTime = DateTime.UtcNow;
            service.process(new Mock<BoardGame>().Object);
            TimeSpan duration = TimeSpan.FromSeconds(1.5);
            Assert.IsTrue((DateTime.UtcNow - startTime) > duration);
        }
    }
}
