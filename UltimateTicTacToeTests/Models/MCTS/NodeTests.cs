using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.MCTS;

namespace UltimateTicTacToeTests.Models.MCTS
{
    [TestClass]
    public class NodeTests
    {
        Node node;

        [TestInitialize]
        public void Setup()
        {
            node = new Node(null, null, null);
        }

        [TestMethod]
        public void WillReturnTheParentNode()
        {
            Mock<INode> mockNode = new Mock<INode>();
            node = new Node(null, mockNode.Object, null);
            Assert.AreSame(mockNode.Object, node.getParent());
        }

        [TestMethod]
        public void WillReturnIsLeafIfExpandHasNotBeenCalled()
        {
            Assert.IsTrue(node.isLeaf());
        }

        [TestMethod]
        public void WillReturnFalseIfExpandHasBeenCalled()
        {
            node.expand();
            Assert.IsFalse(node.isLeaf());
        }
    }
}
