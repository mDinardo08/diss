using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.WinCheck;

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
            CompositeGame game = new TicTacToe(mockChecker.Object);
            game.setBoard(new List<List<BoardGame>>());
            mockChecker.Setup(x => x.checkForWin(game)).Returns((Player)null);
            game.getWinner();
        }

        [TestMethod]
        public void WillCallWinCheckerToCheckForWin()
        {
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            CompositeGame game = new TicTacToe(mockChecker.Object);
            game.setBoard(new List<List<BoardGame>>());
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
            CompositeGame game = new TicTacToe(null);
            game.setBoard( new List<List<BoardGame>>());
            game.getSector(new Point2D
            {
                X = -1,
                Y = -1
            });
        }

        [TestMethod]
        public void WillReturnTheSectorRequested()
        {
            CompositeGame game = new TicTacToe(null);
            BoardGame temp = new TicTacToe(null);
            game.setBoard( new List<List<BoardGame>>
                {
                    new List<BoardGame>{null, null, null},
                    new List<BoardGame>{null, temp, null},
                    new List<BoardGame>{null, null, null}
                }
            );
            Assert.AreEqual(temp, game.getSector(new Point2D{X = 1,Y = 1}));
        }

        [TestMethod]
        public void WillReturnTheBoardItIsGiven()
        {
            CompositeGame game = new TicTacToe(null);
            BoardGame temp = new TicTacToe(null);
            List<List<BoardGame>> board = new List<List<BoardGame>>
            {
                new List<BoardGame>{temp, temp, temp}
            };
            game.setBoard(board);
            Assert.AreEqual(board, game.getBoard());
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentOutOfRangeException))]
        public void WillThrowIndexOutOfRangeWhenMakingMoveThatDoesNotExistOnBoard()
        {
            CompositeGame game = new TicTacToe(null);
            game.setBoard(new List<List<BoardGame>>());
            game.makeMove(new Move
            {
                possition = new Point2D { X = 1, Y = 2 }
            });
        }

        [TestMethod]
        public void WillPassTheNextMoveToTheSectorDefinedInMove()
        {
            CompositeGame game = new TicTacToe(null);
            Move n = new Move();
            Move m = new Move
            {
                possition = new Point2D { X = 1, Y = 1 },
                next = n
            };
            Mock<BoardGame> mock = new Mock<BoardGame>(MockBehavior.Strict);
            mock.Setup(x => x.makeMove(n));
            game.setBoard(new List<List<BoardGame>>
            {
                new List<BoardGame>{ },
                new List<BoardGame>{null, mock.Object}
            });
            game.makeMove(m);
            Assert.AreEqual(n, mock.Invocations[0].Arguments[0]);
        }
    }
}
