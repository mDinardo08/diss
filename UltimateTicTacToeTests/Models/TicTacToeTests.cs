using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.WinCheck;
using UltimateTicTacToe.Models.Game.Players;

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

        [TestMethod]
        public void WillReturnAMoveInTheFirstCell()
        {
            Mock<BoardGame> mock = new Mock<BoardGame>(MockBehavior.Strict);
            mock.Setup(x => x.getAvailableMoves()).Returns(new List<Move> {
                new Move {
                    owner = new Mock<Player>().Object
                }
            });
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            TicTacToe game = new TicTacToe(mockChecker.Object);
            mockChecker.Setup(x => x.checkForWin(game)).Returns((Player)null);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mock.Object
                }
            };
            List<Move> availableMoves = game.getAvailableMoves();
            Point2D result = availableMoves[0].possition;
            Assert.IsTrue(result.X == 0 && result.Y == 0);
        }

        [TestMethod]
        public void WillNestMovesFromSubgames()
        {
            Move m = new Move
            {
                owner = new Mock<Player>().Object
            };
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move>{m});
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            TicTacToe game = new TicTacToe(mockChecker.Object);
            mockChecker.Setup(x => x.checkForWin(game)).Returns((Player)null);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            List<Move> result = game.getAvailableMoves();
            Assert.AreEqual(m, result[0].next);
        }

        [TestMethod]
        public void WillNestAllMovesFromSubGame()
        {
            Move m = new Move
            {
                owner = new Mock<Player>().Object
            };
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move> { m, m, null });
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            TicTacToe game = new TicTacToe(mockChecker.Object);
            mockChecker.Setup(x => x.checkForWin(game)).Returns((Player)null);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            List<Move> result = game.getAvailableMoves();
            Assert.IsTrue(!result.TrueForAll((Move x) => x.next == m));
        }

        [TestMethod]
        public void WillAssignTheCorrectPossitionInTheXDirection()
        {
            Move m = new Move
            {
                owner = new Mock<Player>().Object
            };
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move> {m});

            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            TicTacToe game = new TicTacToe(mockChecker.Object);
            mockChecker.Setup(x => x.checkForWin(game)).Returns((Player)null);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object, mockGame.Object
                }
            };
            Point2D result = game.getAvailableMoves()[1].possition;
            Assert.IsTrue(result.X == 1);
        }

        [TestMethod]
        public void WillAssignTheCorrectPossitionInTheYDirection()
        {
            Move m = new Move
            {
                owner = new Mock<Player>().Object
            };
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move> { m });

            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            TicTacToe game = new TicTacToe(mockChecker.Object);
            mockChecker.Setup(x => x.checkForWin(game)).Returns((Player)null);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                },
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            Point2D result = game.getAvailableMoves()[1].possition;
            Assert.IsTrue(result.Y == 1);
        }

        [TestMethod]
        public void WillReturnEmptyListIfGameIsOver()
        {
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            TicTacToe game = new TicTacToe(mockChecker.Object);
            mockChecker.Setup(x => x.checkForWin(game)).Returns(new Mock<Player>().Object);
            List<Move> result = game.getAvailableMoves();
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void WillReturnAllMovesIfPreviousMovePointsToAFinishedBoard()
        {
            Move move = new Move
            {
                possition = new Point2D
                {
                    X = 0,
                    Y = 0
                },
                next = new Move
                {
                    possition = new Point2D
                    {
                        X = 0,
                        Y = 0
                    }
                }
            };
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            TicTacToe game = new TicTacToe(mockChecker.Object);
            mockChecker.Setup(x => x.checkForWin(game)).Returns((Player) null);
            Mock<BoardGame> mockFinishedGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockFinishedGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move>());
            mockFinishedGame.Setup(x => x.makeMove(move.next));
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move> { new Move() });
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockFinishedGame.Object, mockGame.Object, mockGame.Object
                },
                new List<BoardGame>
                {
                    mockGame.Object, mockGame.Object, mockGame.Object
                },
                new List<BoardGame>
                {
                    mockGame.Object, mockGame.Object, mockGame.Object
                }
            };
            game.makeMove(move);
            List<Move> result = game.getAvailableMoves();
            Assert.IsTrue(result.Count == 8);
        }

        [TestMethod]
        public void WillFilterMovesToThoseInTheSubBoardThatMatchTheNextMovesPossition()
        {
            Move move = new Move
            {
                possition = new Point2D
                {
                    X = 0,
                    Y = 0
                },
                next = new Move
                {
                    possition = new Point2D
                    {
                        X = 1,
                        Y = 0
                    }
                }
            };
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            TicTacToe game = new TicTacToe(mockChecker.Object);
            mockChecker.Setup(x => x.checkForWin(game)).Returns((Player)null);
            Mock<BoardGame> mockFinishedGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockFinishedGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move>());
            mockFinishedGame.Setup(x => x.makeMove(move.next));
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Strict);
            mockGame.Setup(x => x.getAvailableMoves()).Returns(new List<Move> { new Move() });
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockFinishedGame.Object, mockGame.Object, mockGame.Object
                },
                new List<BoardGame>
                {
                    mockGame.Object, mockGame.Object, mockGame.Object
                },
                new List<BoardGame>
                {
                    mockGame.Object, mockGame.Object, mockGame.Object
                }
            };
            game.makeMove(move);
            List<Move> result = game.getAvailableMoves();
            Assert.AreEqual(1, result.Count);
        }
    }
}
