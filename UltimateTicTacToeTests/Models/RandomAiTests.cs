
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
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
            player.name = "I am so random XD !!";
            Assert.IsTrue(String.Equals("I am so random XD !!", player.getName()));
        }

        [TestMethod]
        public void WillPassTheLengthOfThePossibleMovesListAndZeroToRandomService()
        {
            Mock<IRandomService> mock = new Mock<IRandomService>(MockBehavior.Strict);
            mock.Setup(x => x.getRandomNummberBetween(0, 3)).Returns(0);
            player = new RandomAi(mock.Object);
            Move move = new Move();
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move>
            {
                move , new Move(), new Move()
            });
            mockGame.Setup(x => x.makeMove(move));
            player.makeMove(mockGame.Object);
            mock.Verify(m => m.getRandomNummberBetween(0, 3), Times.Once);
        }

        [TestMethod]
        public void WillUseTheIndexReturnedByTheRandomServiceToPickItsMove()
        {
            Mock<IRandomService> mock = new Mock<IRandomService>(MockBehavior.Strict);
            mock.Setup(x => x.getRandomNummberBetween(0, 3)).Returns(1);
            player = new RandomAi(mock.Object);
            Move move = new Move();
            move.possition = new Point2D
            {
                X = 1,
                Y = 1
            };
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move>
            {
                new Move(), move, new Move()
            });
            mockGame.Setup(x => x.makeMove(move));
            player.makeMove(mockGame.Object);
            mockGame.Verify(x => x.makeMove(move), Times.Once);
        }

        [TestMethod]
        public void WillReturnTheMutatedBoard()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move> { new Move() });
            Mock<IRandomService> mock = new Mock<IRandomService>(MockBehavior.Loose);
            player = new RandomAi(mock.Object);
            BoardGame result = player.makeMove(mockGame.Object);
            Assert.AreEqual(mockGame.Object, result);
        }
    }
}
