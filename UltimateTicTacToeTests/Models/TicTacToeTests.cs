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
            game.setBoard(new List<List<BoardGame>>());
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
            game.setBoard(new List<List<BoardGame>>
                {
                    new List<BoardGame>{null, null, null},
                    new List<BoardGame>{null, temp, null},
                    new List<BoardGame>{null, null, null}
                }
            );
            Assert.AreEqual(temp, game.getSector(new Point2D { X = 1, Y = 1 }));
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
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WillThrowIndexOutOfRangeWhenMakingMoveThatDoesNotExistOnBoard()
        {
            CompositeGame game = new TicTacToe(null);
            game.setBoard(new List<List<BoardGame>>());
            game.makeMove(new Move
            {
                possition = new Point2D { X = 1, Y = 2 },
                next = new Move()
            });
        }

        [TestMethod]
        public void WillPassTheNextMoveToTheSectorDefinedInMove()
        {
            CompositeGame game = new TicTacToe(new Mock<IWinChecker>().Object);
            Move n = new Move();
            Move m = new Move
            {
                possition = new Point2D { X = 1, Y = 1 },
                next = n
            };
            Mock<BoardGame> mock = new Mock<BoardGame>(MockBehavior.Loose);
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
        public void WillSetItsBoardFilterToTheNextPossition()
        {
            Move move = new Move();
            move.next = new Move
            {
                possition = new Point2D
                {
                    X = 10,
                    Y = 10
                }
            };
            move.possition = new Point2D
            {
                X = 0,
                Y = 0
            };
            TicTacToe game = new TicTacToe(new Mock<IWinChecker>().Object);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    new Mock<BoardGame>().Object
                }
            };
            game.validateBoard(move);
            Assert.AreEqual(move.next.possition, game.getBoardFilter());
        }

        [TestMethod]
        public void WillPassTheNextMoveToThePossitionOfTheTopMove()
        {
            Move move = new Move();
            move.next = new Move
            {
                possition = new Point2D
                {
                    X = 10,
                    Y = 10
                }
            };
            move.possition = new Point2D
            {
                X = 0,
                Y = 0
            };
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.validateBoard(move.next)).Verifiable();
            TicTacToe game = new TicTacToe(new Mock<IWinChecker>().Object);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            game.validateBoard(move);
            mockGame.Verify();
        }

        [TestMethod]
        public void WillNotThrowAnExceptionIfNextIsNull()
        {
            Move move = new Move();
            TicTacToe game = new TicTacToe(new Mock<IWinChecker>().Object);
            try
            {
                game.validateBoard(move);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void WillSetTheOwnerToWhateverTheWinCheckerReturnsOnValidation()
        {
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>();
            Mock<Player> mockPlayer = new Mock<Player>();
            mockChecker.Setup(x => x.checkForWin(It.IsAny<BoardGame>()))
                .Returns(mockPlayer.Object);
            TicTacToe game = new TicTacToe(mockChecker.Object);
            game.validateBoard(new Move());
            Assert.AreEqual(mockPlayer.Object, game.owner);
        }

        [TestMethod]
        public void WillReturnFalseIfNoOwnerIsSet()
        {
            TicTacToe game = new TicTacToe(null);
            Assert.IsFalse(game.isWon());
        }

        [TestMethod]
        public void WillReturnTrueIfOwnerIsSet()
        {
            TicTacToe game = new TicTacToe(null);
            game.owner = new Mock<Player>().Object;
            Assert.IsTrue(game.isWon());
        }

        [TestMethod]
        public void WillReturnEmptyListIfGameIsWon()
        {
            TicTacToe game = new TicTacToe(null);
            game.owner = new Mock<Player>().Object;
            List<Move> result = game.getAvailableMoves();
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void WillCallForAvailableMovesFromSubBoardsIfGameIsNotWon()
        {
            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>())
                .Verifiable();
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            game.getAvailableMoves();
            mockGame.Verify(x => x.getAvailableMoves(), Times.Once);
        }

        [TestMethod]
        public void WillCallAlongRowsForAvailableMoves()
        {
            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>())
                .Verifiable();
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object, mockGame.Object
                }
            };
            game.getAvailableMoves();
            mockGame.Verify(x => x.getAvailableMoves(), Times.Exactly(2));
        }

        [TestMethod]
        public void WillCallAlongColumnsForAvailableMoves()
        {
            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>())
                .Verifiable();
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
            game.getAvailableMoves();
            mockGame.Verify(x => x.getAvailableMoves(), Times.Exactly(2));
        }

        [TestMethod]
        public void WillReturnAListThatIsTheSumOfAllSubBoardLists()
        {
            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move()
                })
                .Verifiable();
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                }
            };
            List<Move> result = game.getAvailableMoves();
            Assert.IsTrue(result.Count == 9);
        }

        [TestMethod]
        public void WillCheckIfFilteredSubBoardIsWon()
        {

            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.isWon())
                .Returns(false)
                .Verifiable();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>());
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            game.boardFilter = new Point2D
            {
                X = 0,
                Y = 0
            };
            List<Move> result = game.getAvailableMoves();
            mockGame.Verify(x => x.isWon(), Times.Once);
        }

        [TestMethod]
        public void WillReturnAllSubMoveBoardsIfFilteredSubBoardIsWon()
        {

            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.isWon())
                .Returns(true);
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move()
                });
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                }
            };
            game.boardFilter = new Point2D
            {
                X = 0,
                Y = 0
            };
            List<Move> result = game.getAvailableMoves();
            Assert.IsTrue(result.Count == 9);
        }

        [TestMethod]
        public void WillReturnMovesFromAllSubBoardsIfFilterExistsAndFilteredBoardIsDrawn()
        {

            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.isWon())
                .Returns(false);
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move()
                });
            mockGame.Setup(x => x.isDraw()).Returns(true);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                }
            };
            game.boardFilter = new Point2D
            {
                X = 0,
                Y = 0
            };
            List<Move> result = game.getAvailableMoves();
            Assert.IsTrue(result.Count == 9);
        }

        [TestMethod]
        public void WillReturnTheSameNumberOfMovesAsFilteredIfFilteredBoardIsNotComplete()
        {
            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.isWon())
                .Returns(false);
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move(), new Move()
                });
            mockGame.Setup(x => x.isDraw()).Returns(false);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                }
            };
            game.boardFilter = new Point2D
            {
                X = 0,
                Y = 0
            };
            List<Move> result = game.getAvailableMoves();
            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public void WillNestTheMovesIfFilterExists()
        {
            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.isWon())
                .Returns(false);
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move()
                });
            mockGame.Setup(x => x.isDraw()).Returns(false);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                }
            };
            List<Move> result = game.getAvailableMoves();
            Assert.IsTrue(result[0].possition.X == 0 && result[0].possition.Y == 0); ;
        }

        [TestMethod]
        public void WillNestMovesWithBoardFilter()
        {
            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.isWon())
                .Returns(false);
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move()
                });
            mockGame.Setup(x => x.isDraw()).Returns(false);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                },new List<BoardGame>
                {
                    mockGame.Object,mockGame.Object,mockGame.Object
                }
            };
            game.boardFilter = new Point2D
            {
                X = 1,
                Y = 1
            };
            List<Move> result = game.getAvailableMoves();
            Assert.IsTrue(result[0].possition.X == 1 && result[0].possition.Y == 1); 
        }

        [TestMethod]
        public void WillReturnFalseIfThereAreAvailableMoves()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move()
                });
            TicTacToe game = new TicTacToe(null);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            bool result = game.isDraw();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WillReturnFalseIfTheOwnerIsSetAndThereAreMovesAvailable()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>
                {
                    new Move()
                });
            TicTacToe game = new TicTacToe(null);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            game.owner = new Mock<Player>().Object;
            bool result = game.isDraw();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WillReturnTrueIfOwnerIsNotSetAndNoMovesAreAvailable()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getAvailableMoves())
                .Returns(new List<Move>());
            TicTacToe game = new TicTacToe(null);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            bool result = game.isDraw();
            Assert.IsTrue(result);
        }
    }
}
