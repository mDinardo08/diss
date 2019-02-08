
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class RandomAiTests
    {
        RandomAi player;

        [TestInitialize()]
        public void Setup()
        {
            player = new RandomAi(null);
        }

        [TestMethod]
        public void WillRetrunWhateverNameItWasInitialisedWith()
        {
            (player as AbstractPlayer).name = "I am so random XD !!";
            Assert.IsTrue(String.Equals("I am so random XD !!", player.getName()));
        }

        [TestMethod]
        public void WillPassTheLengthOfThePossibleMovesListAndZeroToRandomService()
        {
            Mock<IRandomService> mock = new Mock<IRandomService>(MockBehavior.Strict);
            mock.Setup(x => x.getRandomNumberBetween(0, 3)).Returns(0);
            player = new RandomAi(mock.Object);
            Move move = new Move();
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockGame.Setup(x => x.makeMove(move));
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.getMove())
                .Returns(new Move());
            List<INode> nodes = new List<INode>
            {
                mockNode.Object, mockNode.Object, mockNode.Object
            };
            player.makeMove(mockGame.Object, nodes);
            mock.Verify(m => m.getRandomNumberBetween(0, 3), Times.Once);
        }

        [TestMethod]
        public void WillUseTheIndexReturnedByTheRandomServiceToPickItsMove()
        {
            Mock<IRandomService> mock = new Mock<IRandomService>(MockBehavior.Strict);
            mock.Setup(x => x.getRandomNumberBetween(0, 3)).Returns(1);
            player = new RandomAi(mock.Object);
            Move move = new Move();
            move.possition = new Point2D
            {
                X = 1,
                Y = 1
            };
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.getMove())
                .Returns(move);
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockGame.Setup(x => x.makeMove(move));
            INode result = player.makeMove(mockGame.Object, new List<INode> { null, mockNode.Object, null});
            Assert.AreSame(result.getMove(), move);
        }


        [TestMethod]
        public void WillSetItselfAsTheOwnerOfTheMove()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<IRandomService> mock = new Mock<IRandomService>(MockBehavior.Loose);
            player = new RandomAi(mock.Object);
            player.colour = (PlayerColour)1000;
            Mock<INode> mockNode = new Mock<INode>();
            Move move = new Move();
            mockNode.Setup(x => x.getMove())
                .Returns(move);
            INode result = player.makeMove(mockGame.Object, new List<INode> { mockNode.Object });
            Assert.IsTrue(result.getMove().owner == player.colour); 
        }

        [TestMethod]
        public void WillReturnTheirPlayerType()
        {
            (player as AbstractPlayer).type = PlayerType.RANDOM;
            Assert.AreEqual(player.getPlayerType(), PlayerType.RANDOM);
        }
    }
}
