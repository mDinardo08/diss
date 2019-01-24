using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.Game.WinCheck;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Services
{
    [TestClass]
    public class UltimateTicTacToeCreationServiceTests
    {
        BoardCreationService service;
        List<List<JObject>> JNestedBoard;
        JObject JBoard;
        [TestInitialize()]
        public void Setup()
        {
            service = new UltimateTicTacToeCreationService(null, null);
            JBoard = new JObject();
            JBoard.Add("board", JToken.FromObject(new List<List<BoardGame>>()));
            JNestedBoard = new List<List<JObject>>
            {
                new List<JObject>
                {
                    JBoard
                }
            };
        }

        [TestMethod]
        public void WillCeateAnUltimateTicTacToeBoard()
        {
            List<List<JObject>> board = new List<List<JObject>>
            {
                new List<JObject>
                {
                    JObject.FromObject(new TicTacToe(null))
                }
            };

            Mock<IPlayerCreationService> mockService = new Mock<IPlayerCreationService>(MockBehavior.Loose);
            mockService.Setup(x => x.createPlayer(It.IsAny<JObject>()));
            service = new UltimateTicTacToeCreationService(null, mockService.Object);
            BoardGame game = service.createBoardGame(new BoardGameDTO { game = board, lastMove = new Move() });
            Assert.IsTrue(game is TicTacToe);
        }

        [TestMethod]
        [ExpectedException(typeof(NoWinnerException))]
        public void WillSupplyAWinCheckerToTheGame()
        {
            Mock<IWinChecker> mockHandler = new Mock<IWinChecker>(MockBehavior.Loose);
            List<List<JObject>> board = JNestedBoard;
            Mock<IPlayerCreationService> mockService = new Mock<IPlayerCreationService>(MockBehavior.Loose);
            mockService.Setup(x => x.createPlayer(It.IsAny<JObject>()));
            service = new UltimateTicTacToeCreationService(mockHandler.Object, mockService.Object);
            TicTacToe game = service.createBoardGame(new BoardGameDTO { game = board, lastMove = new Move() }) as TicTacToe;
            game.getWinner();
        }

        [TestMethod]
        public void WillPopulateAOneByOneGame()
        {
            List<List<JObject>> board = new List<List<JObject>> {
                new List<JObject>{
                    JObject.FromObject(new Tile())
                }
            };
            Mock<IPlayerCreationService> mockService = new Mock<IPlayerCreationService>(MockBehavior.Loose);
            mockService.Setup(x => x.createPlayer(It.IsAny<JObject>()));
            service = new UltimateTicTacToeCreationService(null, mockService.Object);
            TicTacToe game = service.createBoardGame(new BoardGameDTO { game = board, lastMove = new Move() }) as TicTacToe;
            Assert.IsTrue(game.getBoard()[0][0] is Tile);
        }

        [TestMethod]
        public void WillPopulateALongRows()
        {
            List<List<JObject>> board = JNestedBoard;
            board[0].Add(JBoard);
            Mock<IPlayerCreationService> mockService = new Mock<IPlayerCreationService>(MockBehavior.Loose);
            mockService.Setup(x => x.createPlayer(It.IsAny<JObject>()));
            service = new UltimateTicTacToeCreationService(null, mockService.Object);
            TicTacToe game = service.createBoardGame(new BoardGameDTO { game = board, lastMove = new Move() }) as TicTacToe;
            Assert.IsTrue(game.getBoard()[0].Count == 2);
        }

        [TestMethod]
        public void WillPopulateAlongColumns()
        {
            List<List<JObject>> board = JNestedBoard;
            board.Add(new List<JObject>
            {
                JBoard
            });
            Mock<IPlayerCreationService> mockService = new Mock<IPlayerCreationService>(MockBehavior.Loose);
            mockService.Setup(x => x.createPlayer(It.IsAny<JObject>()));
            service = new UltimateTicTacToeCreationService(null, mockService.Object);
            TicTacToe game = service.createBoardGame(new BoardGameDTO { game = board, lastMove = new Move() }) as TicTacToe;
            Assert.IsTrue(game.getBoard().Count == 2);
        }

        [TestMethod]
        public void WillCorrectlyNestGamesOfTicTacToe()
        {
            JObject ticTacToe = new JObject();
            ticTacToe.Add("board", JToken.FromObject(new List<List<BoardGame>>()));
            List<List<JObject>> board = JNestedBoard;
            TicTacToe game = service.createBoardGame(new BoardGameDTO { game = board, lastMove = new Move() }) as TicTacToe;
            Assert.IsTrue(game.getBoard()[0][0] is TicTacToe);
        }

        [TestMethod]
        public void NestedGamesWillHaveAWinChecker()
        {
            Mock<IWinChecker> mockHandler = new Mock<IWinChecker>(MockBehavior.Loose);
            mockHandler.Setup(x => x.checkForWin(It.IsAny<BoardGame>())).Returns(new Mock<Player>().Object);
            service = new UltimateTicTacToeCreationService(mockHandler.Object, null);
            JObject ticTacToe = new JObject();
            ticTacToe.Add("board", JToken.FromObject(new List<List<BoardGame>>()));
            List<List<JObject>> board = JNestedBoard;
            TicTacToe game = service.createBoardGame(new BoardGameDTO { game = board, lastMove = new Move()}) as TicTacToe;
            try
            {
                (game.getBoard()[0][0] as TicTacToe).getWinner();
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception but got: " + e);
            }
        }

        [TestMethod]
        public void WillPassPlayerJObjectToPlayerCreationService()
        {
            Mock<IPlayerCreationService> mockService = new Mock<IPlayerCreationService>(MockBehavior.Loose);
            mockService.Setup(x => x.createPlayer(It.IsAny<JObject>())).Verifiable();
            service = new UltimateTicTacToeCreationService(null, mockService.Object);
            List<List<JObject>> board = new List<List<JObject>>
            {
                new List<JObject>
                {
                    JObject.FromObject(new Tile
                    {
                        owner = new RandomAi(null)
                    })
                }
            };
            TicTacToe game = service.createBoardGame(new BoardGameDTO { game = board, lastMove = new Move() }) as TicTacToe;
            mockService.Verify();
        }

        [TestMethod]
        public void WillSetTheReturnedValueFromPlayerCreationToTheTile()
        {
            Mock<IPlayerCreationService> mockService = new Mock<IPlayerCreationService>(MockBehavior.Loose);
            Tile tile = new Tile();
            Player p = new RandomAi(null);
            tile.owner = p;
            mockService.Setup(x => x.createPlayer(It.IsAny<JObject>())).Returns(p);
            service = new UltimateTicTacToeCreationService(null, mockService.Object);
            List<List<JObject>> board = new List<List<JObject>>
            {
                new List<JObject>
                {
                    JObject.FromObject(tile)
                }
            };
            TicTacToe game = service.createBoardGame(new BoardGameDTO { game = board, lastMove = new Move() }) as TicTacToe;
            Assert.AreEqual(p, (game.board[0][0] as Tile).owner);
        }

        [TestMethod]
        public void WillReturnATicTacToeGame()
        {
            BoardGame result = service.createBoardGame(3);
            Assert.IsTrue(result is TicTacToe);
        }

        [TestMethod]
        public void WillReturnAOuterListOfDesiredHeight()
        {
            TicTacToe result = service.createBoardGame(3) as TicTacToe;
            Assert.IsTrue(result.board.Count == 3);
        }

        [TestMethod]
        public void WillReturnAInnerListWhichCountIsSize()
        {
            TicTacToe result = service.createBoardGame(3) as TicTacToe;
            Assert.IsTrue(result.board[0].Count == 3);
        }

        [TestMethod]
        public void WillSpawnTicTacToeBoardsAsInternalBoards()
        {
            TicTacToe result = service.createBoardGame(3) as TicTacToe;
            Assert.IsTrue(result.board[0][0] is TicTacToe);
        }

        [TestMethod]
        public void WillPlaceABoardOfTheCorrectHeightInTheInternalGames()
        {
            TicTacToe result = service.createBoardGame(3) as TicTacToe;
            TicTacToe internalGame = result.board[0][0] as TicTacToe;
            Assert.IsTrue(internalGame.board.Count == 3);
        }

        [TestMethod]
        public void WillPlaceABoardOfTheCorrectWidth()
        {
            TicTacToe result = service.createBoardGame(3) as TicTacToe;
            TicTacToe internalGame = result.board[0][0] as TicTacToe;
            Assert.IsTrue(internalGame.board[0].Count == 3);
        }

        [TestMethod]
        public void WillPlaceNewTilesInTheBoard()
        {
            TicTacToe result = service.createBoardGame(3) as TicTacToe;
            TicTacToe internalGame = result.board[0][0] as TicTacToe;
            Assert.IsTrue(internalGame.board[0][0] is Tile);
        }

        [TestMethod]
        [ExpectedException(typeof(NoWinnerException))]
        public void WillPassAWinCheckerToTheTicTacToeGameOuter()
        {
            Mock<IWinChecker> mockHandler = new Mock<IWinChecker>();
            service = new UltimateTicTacToeCreationService(mockHandler.Object, null);
            TicTacToe result = service.createBoardGame(3) as TicTacToe;
            TicTacToe internalGame = result.board[0][0] as TicTacToe;
            internalGame.getWinner();
        }
    }
}
