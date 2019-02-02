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
            mockChecker.Setup(x => x.checkForWin(game)).Returns((PlayerColour?)null);
            game.getWinner();
        }

        [TestMethod]
        public void WillCallWinCheckerToCheckForWin()
        {
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>(MockBehavior.Strict);
            CompositeGame game = new TicTacToe(mockChecker.Object);
            game.setBoard(new List<List<BoardGame>>());
            mockChecker.Setup(x => x.checkForWin(game)).Returns((PlayerColour?)null);
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
                new List<BoardGame>{new Mock<BoardGame>().Object, mock.Object}
            });
            game.makeMove(m);
            Assert.AreEqual(n, mock.Invocations[0].Arguments[0]);
        }

        [TestMethod]
        public void WillCallToValidateSubBoards()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.validateBoard()).Verifiable();
            TicTacToe game = new TicTacToe(new Mock<IWinChecker>().Object);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            game.validateBoard();
            mockGame.Verify();
        }

        [TestMethod]
        public void WillCallToValidateBoardAcrossRow()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.validateBoard()).Verifiable();
            TicTacToe game = new TicTacToe(new Mock<IWinChecker>().Object);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object, mockGame.Object
                }
            };
            game.validateBoard();
            mockGame.Verify(x => x.validateBoard(), Times.Exactly(2));
        }

        [TestMethod]
        public void WillCallToValidateBoardAcrossColumns()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.validateBoard()).Verifiable();
            TicTacToe game = new TicTacToe(new Mock<IWinChecker>().Object);
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
            game.validateBoard();
            mockGame.Verify(x => x.validateBoard(), Times.Exactly(2));
        }

        [TestMethod]
        public void WillSetOwnerToWhateverPlayerTheWinCheckerReturns()
        {
            Mock<IWinChecker> mockChecker = new Mock<IWinChecker>();
            Mock<Player> mockPlayer = new Mock<Player>();
            mockChecker.Setup(x => x.checkForWin(It.IsAny<BoardGame>()))
                .Returns(0);
            TicTacToe game = new TicTacToe(mockChecker.Object);
            game.board = new List<List<BoardGame>>();
            game.validateBoard();
            Assert.AreEqual(game.owner, (PlayerColour)0);
        }

        [TestMethod]
        public void WillSetNextPossitionAsBoardFilter()
        {
            Move move = new Move();
            Move next = new Move();
            next.possition = new Point2D
            {
                X = 0,
                Y = 0
            };
            move.next = next;
            TicTacToe game = new TicTacToe(null);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    new Mock<BoardGame>().Object
                }
            };
            game.registerMove(move);
            Assert.AreEqual(next.possition, game.boardFilter);
        }

        [TestMethod]
        public void WillNotSetBoardFilterIfNextPossitionIsNull()
        {
            Move move = new Move();
            move.next = new Move();
            TicTacToe game = new TicTacToe(null);
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    new Mock<BoardGame>().Object
                }
            };
            game.registerMove(move);
            Assert.IsNull(game.boardFilter);
        }

        [TestMethod]
        public void WillCallTheNextPossitionBoardToRegisterTheMove()
        {
            Move move = new Move();
            Move next = new Move();
            next.possition = new Point2D
            {
                X = 1,
                Y = 1
            };
            move.next = next;
            TicTacToe game = new TicTacToe(null);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.registerMove(move.next))
                .Verifiable();
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>(),
                new List<BoardGame>
                {
                    null, mockGame.Object
                }
            };
            game.registerMove(move);
            mockGame.Verify();
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
            game.owner = 0;
            Assert.IsTrue(game.isWon());
        }

        [TestMethod]
        public void WillReturnEmptyListIfGameIsWon()
        {
            TicTacToe game = new TicTacToe(null);
            game.owner = 0;
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
            game.owner = 0;
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

        [TestMethod]
        public void WillReturnAnObjectWhichIsNotTheSameAsItself()
        {
            TicTacToe game = new TicTacToe(null);
            game.board = new List<List<BoardGame>>();
            game.boardFilter = new Point2D();
            Assert.AreNotSame(game, game.Clone());
        }

        [TestMethod]
        public void WillReturnAnInstanceOfTicTacToe()
        {
            TicTacToe game = new TicTacToe(null);
            game.board = new List<List<BoardGame>>();
            game.boardFilter = new Point2D();
            Assert.IsTrue(game.Clone() is TicTacToe);
        }

        [TestMethod]
        public void WillCallCloneOnSubboard()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.Clone())
                .Returns(null)
                .Verifiable();
            TicTacToe game = new TicTacToe(null);
            game.boardFilter = new Point2D();
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            game.Clone();
            mockGame.Verify();
        }

        [TestMethod]
        public void WillCallCloneOnSubboardAlongRow()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.Clone())
                .Returns(null)
                .Verifiable();
            TicTacToe game = new TicTacToe(null);
            game.boardFilter = new Point2D();
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object, mockGame.Object
                }
            };
            game.Clone();
            mockGame.Verify(x => x.Clone(), Times.Exactly(2));
        }

        [TestMethod]
        public void WillCallCloneAcrossColumns()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.Clone())
                .Returns(null)
                .Verifiable();
            TicTacToe game = new TicTacToe(null);
            game.boardFilter = new Point2D();
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
            game.Clone();
            mockGame.Verify(x => x.Clone(), Times.Exactly(2));
        }

        [TestMethod]
        public void WillAssignANewBoardToTheReturnedObject()
        {
            TicTacToe game = new TicTacToe(null);
            game.board = new List<List<BoardGame>>();
            game.boardFilter = new Point2D();
            TicTacToe result = game.Clone() as TicTacToe;
            Assert.AreNotSame(result.board, game.board);
        }

        [TestMethod]
        public void WillAssignTheClonedSubBoardsToItsResultBoard()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Mock<BoardGame> mockCloned = new Mock<BoardGame>();
            mockGame.Setup(x => x.Clone())
                .Returns(mockCloned.Object);
            TicTacToe game = new TicTacToe(null);
            game.boardFilter = new Point2D();
            game.board = new List<List<BoardGame>>
            {
                new List<BoardGame>
                {
                    mockGame.Object
                }
            };
            TicTacToe result = game.Clone() as TicTacToe;
            Assert.AreSame(mockCloned.Object, result.board[0][0]);
        }

        [TestMethod]
        public void OwnerWillNotBeSame()
        {
            TicTacToe game = new TicTacToe(null);
            game.board = new List<List<BoardGame>>();
            game.boardFilter = new Point2D();
            PlayerColour colour = (PlayerColour)1000;
            game.owner = colour;
            TicTacToe result = game.Clone() as TicTacToe;
            Assert.AreNotSame(colour, result.owner);
        }

        [TestMethod]
        public void BoardFilterWillNotBeSame()
        {
            TicTacToe game = new TicTacToe(null);
            game.board = new List<List<BoardGame>>();
            Point2D filter = new Point2D();
            game.boardFilter = filter;
            TicTacToe result = game.Clone() as TicTacToe;
            Assert.AreNotSame(filter, result.boardFilter);
        }
    }
}
