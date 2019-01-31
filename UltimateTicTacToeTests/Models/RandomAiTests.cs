
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
        Player player;

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
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move>
            {
                move , new Move(), new Move()
            });
            mockGame.Setup(x => x.makeMove(move));
            player.makeMove(mockGame.Object);
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
        public void WillSetItselfAsTheOwnerOfTheMove()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move> { new Move { owner = 0} });
            Mock<IRandomService> mock = new Mock<IRandomService>(MockBehavior.Loose);
            player = new RandomAi(mock.Object);
            Move expected = new Move { owner = 0};
            mockGame.Setup(x => x.makeMove(It.Is<Move>(m => m.owner == 0))).Verifiable();
            player.makeMove(mockGame.Object);
            mockGame.Verify();
        }

        [TestMethod]
        public void WillReturnTheirPlayerType()
        {
            (player as AbstractPlayer).type = PlayerType.RANDOM;
            Assert.AreEqual(player.getPlayerType(), PlayerType.RANDOM);
        }
    }
}
