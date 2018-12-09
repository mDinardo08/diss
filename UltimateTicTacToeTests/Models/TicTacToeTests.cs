using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.GameClasses;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class TicTacToeTests
    {

        [TestMethod]
        [ExpectedException(typeof(NoWinnerException))]
        public void WillThrowExceptionIfWinCheckerReturnsNULL()
        {

            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            Game game = new TicTacToe(mockChecker.Object);
            game.setBoard(new List<List<Game>>());
            mockChecker.Setup(x => x.checkForWin(game)).Returns((Player)null);
            game.getWinner();
        }

        [TestMethod]
        public void WillCallWinCheckerToCheckForWin()
        {
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            Game game = new TicTacToe(mockChecker.Object);
            game.setBoard(new List<List<Game>>());
            mockChecker.Setup(x => x.checkForWin(game)).Returns((Player)null);
            try
            {
                game.getWinner();
            }
            catch
            {
            }
            mockChecker.Verify(x => x.checkForWin(game), Times.Exactly(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WillThrowIndexOutOfRangeIfGettingInvalidPoint()
        {
            Game game = new TicTacToe(null);
            game.setBoard( new List<List<Game>>());
            game.getSector(new Point
            {
                X = -1,
                Y = -1
            });
        }

        [TestMethod]
        public void WillReturnTheSectorRequested()
        {
            Game game = new TicTacToe(null);
            Game temp = new TicTacToe(null);
            game.setBoard( new List<List<Game>>
                {
                    new List<Game>{null, null, null},
                    new List<Game>{null, temp, null},
                    new List<Game>{null, null, null}
                }
            );
            Assert.AreEqual(temp, game.getSector(new Point{X = 1,Y = 1}));
        }

        [TestMethod]
        public void WillReturnTheBoardItIsGiven()
        {

        }
    }
}
