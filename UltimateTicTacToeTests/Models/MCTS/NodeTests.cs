using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
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
            node = new Node(null, null, null, 0);
        }

        [TestMethod]
        public void WillReturnTheParentNode()
        {
            Mock<INode> mockNode = new Mock<INode>();
            node = new Node(null, mockNode.Object, null, 0);
            Assert.AreSame(mockNode.Object, node.parent);
        }

        [TestMethod]
        public void WillReturnIsLeafIfExpandHasNotBeenCalled()
        {
            Assert.IsTrue(node.isLeaf());
        }

        [TestMethod]
        public void WillReturnFalseIfExpandHasBeenCalledAndChildrenCanBeMade()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>{
                    new Move() }
                );
            mockGame.Setup(x => x.Clone())
                .Returns(mockGame.Object);
            node = new Node(mockGame.Object, null, null, 0);
            node.expand();
            Assert.IsFalse(node.isLeaf());
        }

        [TestMethod]
        public void WillIncreaseNoVisitedWhenRolloutIsCalled()
        {
            Move move = new Move();
            move.owner = PlayerColour.BLUE;
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.Clone())
                .Returns(mockGame.Object);
            mockGame.SetupSequence(x => x.isWon())
                .Returns(false).Returns(true);
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    move
                });
            mockGame.Setup(x => x.getBoard())
                .Returns(new List<List<BoardGame>>());
            node = new Node(mockGame.Object, null, move, 0);
            int before = node.getVisits();
            node.rollOut();
            Assert.IsTrue(before + 1 == node.getVisits());
        }

        [TestMethod]
        public void WillGetAvailableMovesFromBoardOnExpand()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>())
                .Verifiable();
            node = new Node(mockGame.Object, null, null, 0);
            node.expand();
            mockGame.Verify();
        }

        [TestMethod]
        public void WillCloneTheGameForTheAmountOfMovesItHas()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move(), new Move(), new Move(), new Move()
                });
            mockGame.Setup(x => x.Clone())
                .Returns(mockGame.Object)
                .Verifiable();
            node = new Node(mockGame.Object, null, null, 0);
            node.expand();
            mockGame.Verify(x => x.Clone(), Times.Exactly(4));
        }

        [TestMethod]
        public void WillCreateAsManyChildernAsThereAreAvailableMoves()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move(), new Move(), new Move(), new Move()
                });
            mockGame.Setup(x => x.Clone())
                .Returns(mockGame.Object);
            node = new Node(mockGame.Object, null, null, 0);
            node.expand();
            List<INode> children = node.getChildren();
            Assert.IsTrue(children.Count == 4);
        }

        [TestMethod]
        public void WillMakeTheMoveOnTheClonedBoard()
        {
            Mock<BoardGame> cloned = new Mock<BoardGame>();
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Move move = new Move();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    move
                });
            mockGame.Setup(x => x.Clone())
                .Returns(cloned.Object);
            cloned.Setup(x => x.makeMove(move))
                .Verifiable();
            node = new Node(mockGame.Object, null, null, 0);
            node.expand();
            cloned.Verify();
        }

        [TestMethod]
        public void WillPassTheMutatedClonedBoardToItsChild()
        {
            Mock<BoardGame> cloned = new Mock<BoardGame>();
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Move move = new Move();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    move
                });
            mockGame.Setup(x => x.Clone())
                .Returns(cloned.Object);
            node = new Node(mockGame.Object, null, null, 0);
            node.expand();
            List<INode> children = node.getChildren();
            Assert.AreEqual(cloned.Object, (children[0] as Node).game);
        }

        [TestMethod]
        public void WillSetTheMovesOwnerAsTheColourWhichDoesNotMatchItsMoveColour()
        {
            Mock<BoardGame> cloned = new Mock<BoardGame>();
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            bool valid = false;
            Move move = new Move();
            move.owner = PlayerColour.BLUE;
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    move
                });
            mockGame.Setup(x => x.Clone())
                .Returns(cloned.Object);
            cloned.Setup(x => x.makeMove(It.IsAny<Move>()))
                .Callback((Move m) =>
                {
                    valid = m.owner != PlayerColour.BLUE;
                })
                .Verifiable();
            node = new Node(mockGame.Object, null, move, PlayerColour.RED);
            node.expand();
            List<INode> children = node.getChildren();
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void WillAssignColourPassedInIfMoveIsNull()
        {
            Mock<BoardGame> cloned = new Mock<BoardGame>();
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            bool valid = false;
            Move move = new Move();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    move
                });
            mockGame.Setup(x => x.Clone())
                .Returns(cloned.Object);
            cloned.Setup(x => x.makeMove(It.IsAny<Move>()))
                .Callback((Move m) =>
                {
                    valid = m.owner != PlayerColour.BLUE && m.owner != null;
                })
                .Verifiable();
            node = new Node(mockGame.Object, null, null, PlayerColour.RED);
            node.expand();
            List<INode> children = node.getChildren();
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void WillCallToCheckIfTheGameIsWon()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.isWon())
                .Returns(true)
                .Verifiable();
            mockGame.Setup(x => x.Clone())
                .Returns(mockGame.Object);
            mockGame.Setup(x => x.getBoard())
                .Returns(new List<List<BoardGame>>());
            node = new Node(mockGame.Object, null, new Move(), 0);
            node.rollOut();
            mockGame.Verify();
        }

        [TestMethod]
        public void WillCallToCheckIfGameIsDrawn()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.isDraw())
                .Returns(true)
                .Verifiable();
            mockGame.Setup(x => x.Clone())
                .Returns(mockGame.Object);
            mockGame.Setup(x => x.getBoard())
                .Returns(new List<List<BoardGame>>());
            node = new Node(mockGame.Object, null, new Move(), 0);
            node.rollOut();
            mockGame.Verify();
        }

        [TestMethod]
        public void WillCallToCloneTheGame()
        {
            Move move = new Move();
            move.owner = PlayerColour.BLUE;
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.Clone())
                .Returns(mockGame.Object)
                .Verifiable();
            mockGame.Setup(x => x.isWon())
                .Returns(true);
            mockGame.Setup(x => x.getBoard())
                .Returns(new List<List<BoardGame>>());
            node = new Node(mockGame.Object, null, move, 0);
            node.rollOut();
            mockGame.Verify();
        }

        [TestMethod]
        public void WillCallForAvailableMovesOfTheClonedBoard()
        {
            Mock<BoardGame> clone = new Mock<BoardGame>();
            clone.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>())
                .Verifiable();
            clone.SetupSequence(x => x.isWon())
                .Returns(false).Returns(true);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.Clone())
                .Returns(clone.Object);
            clone.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move()
                });
            clone.Setup(x => x.getBoard())
                .Returns(new List<List<BoardGame>>());
            node = new Node(mockGame.Object, null, new Move(), 0);
            node.rollOut();
            clone.Verify();
        }

        [TestMethod]
        public void WillCallForAvailableMovesAsLongAsCloneIsntFinished()
        {
            Mock<BoardGame> clone = new Mock<BoardGame>();
            clone.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>())
                .Verifiable();
            clone.SetupSequence(x => x.isWon())
                .Returns(false).Returns(false).Returns(true);
            clone.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move()
                });
            clone.Setup(x => x.getBoard())
                .Returns(new List<List<BoardGame>>());
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.Clone())
                .Returns(clone.Object);
            node = new Node(mockGame.Object, null, new Move(), 0);
            node.rollOut();
            clone.Verify(x => x.getAvailableMoves(), Times.Exactly(2));
        }

        [TestMethod]
        public void WillReturnOneIfOwnerColourMatchesWinner()
        {
            Mock<BoardGame> game = new Mock<BoardGame>();
            game.Setup(x => x.getWinner())
                .Returns(PlayerColour.RED);
            game.Setup(x => x.isWon())
                .Returns(true);
            node = new Node(null, null, null, PlayerColour.RED);
            int result = node.value(game.Object);
            Assert.AreEqual(1, result);
        }
        
        [TestMethod]
        public void WillReturnANegativeNumberIfGameWasLostOverall()
        {
            Mock<BoardGame> game = new Mock<BoardGame>();
            game.Setup(x => x.getWinner())
                .Returns(PlayerColour.BLUE);
            node = new Node(null, null, null, PlayerColour.RED);
            int result = node.value(game.Object);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void ChildNodesWillNotContainThisNode()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.Clone())
                .Returns(new Mock<BoardGame>().Object);
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move(),new Move(),new Move(),new Move(),new Move(),new Move(),new Move(),new Move(),new Move()
                });
            node = new Node(mockGame.Object, null, null, 0);
            node.expand();
            List<INode> result = node.getChildren();
            Assert.IsFalse(result.Contains(node));
        }
    }
}
