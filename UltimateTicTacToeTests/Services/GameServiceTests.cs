using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.Game.WinCheck;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Services
{
    [TestClass]
    public class GameServiceTests
    {
        IGameService service;

        [TestInitialize()]
        public void Setup()
        {
            service = new GameService(null);
        }

        [TestMethod]
        public void WillSetTheWinningPlayerIfGameIsOver()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<Player> p = new Mock<Player>();
            p.Setup(x => x.getPlayerType()).Returns((PlayerType) 0);
            mockGame.Setup(x => x.getWinner()).Returns(0);
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>());
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>());
            Assert.AreEqual((PlayerColour)0, result.Winner);
        }

        [TestMethod]
        public void WillHaveTheAiMakeAMoveIfGameIsNotOver()
        {
            Mock<BoardGame> mockCloned = new Mock<BoardGame>();
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            mockGame.Setup(x => x.Clone())
                .Returns(mockCloned.Object);
            Mock<Player> p = new Mock<Player>();
            p.Setup(x => x.makeMove(mockCloned.Object))
                .Returns(new Move())
                .Verifiable();
            mockGame.Setup(x => x.getWinner())
                .Throws(new NoWinnerException());
            mockGame.SetupSequence(x => x.getWinner())
                .Throws(new NoWinnerException());
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>());
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>());
            p.Verify();
        }

        [TestMethod]
        public void WillReturnTheAisTheWinnerIfItsMoveWinsTheGame()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<Player> p = new Mock<Player>();
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(new Move());
            mockGame.Setup(x => x.getWinner()).Returns(0);
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>());
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>());
            Assert.IsTrue(result.Winner == 0);
        }

        [TestMethod]
        public void WillAssignTheNextPlayerAsTheCurrentPlayerOnTheReturnedDto()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<Player> p = new Mock<Player>();
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(new Move());
            p.Setup(x => x.getPlayerType()).Returns(PlayerType.RANDOM);
            p.Setup(x => x.getName()).Returns("hello");
            p.Setup(x => x.getColour()).Returns(PlayerColour.RED);
            Mock<Player> next = new Mock<Player>();
            next.Setup(x => x.getPlayerType()).Returns((PlayerType)1000);
            next.Setup(x => x.getName()).Returns("hello");
            next.Setup(x => x.getColour()).Returns(PlayerColour.BLUE);
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            mockGame.SetupSequence(x => x.getWinner()).Throws(new NoWinnerException()).Returns(0);
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>());
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>
            {
                p.Object, next.Object
            });
            Assert.IsTrue(result.cur["type"].ToObject<PlayerType>() == (PlayerType)1000);
        }

        [TestMethod]
        public void WillAddEmpty2DListOfJObject()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<Player> p = new Mock<Player>();
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(new Move());
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>());
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>());
            Assert.IsTrue(result.game.Count is 0);
        }

        [TestMethod]
        public void WillAddOneTile()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<Player> p = new Mock<Player>();
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(new Move());
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>> {
                new List<BoardGame>
                {
                    new Tile()
                }
            });
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>());
            Assert.IsTrue(result.game[0].Count is 1);
        }

        [TestMethod]
        public void WillAddOneTileWithPropertiesPopulated()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<Player> p = new Mock<Player>();
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(new Move());
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>> {
                new List<BoardGame>
                {
                    new Tile
                    {
                        owner = 0
                    }
                }
            });
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>());
            Assert.IsNotNull(result.game[0][0]["owner"]);
        }

        [TestMethod]
        public void WillAddTilesAlongWidth()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<Player> p = new Mock<Player>();
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(new Move());
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>> {
                new List<BoardGame>
                {
                    new Tile(), new Tile()
                }
            });
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>());
            Assert.IsTrue(result.game[0].Count is 2);
        }

        [TestMethod]
        public void WillPopulateOverHight()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<Player> p = new Mock<Player>();
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(new Move());
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>> {
                new List<BoardGame>
                {
                    new Tile()
                },
                new List<BoardGame>
                {
                    new Tile()
                }
            });
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>());
            Assert.IsTrue(result.game.Count is 2);
        }

        [TestMethod]
        public void WillAddTheNameOfTheNewCurPlayer()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<Player> p = new Mock<Player>();
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(new Move());
            p.Setup(x => x.getPlayerType()).Returns(PlayerType.RANDOM);
            p.Setup(x => x.getName()).Returns("name");
            p.Setup(x => x.getColour()).Returns(PlayerColour.BLUE);
            Mock<Player> next = new Mock<Player>();
            next.Setup(x => x.getName()).Returns("name");
            next.Setup(x => x.getPlayerType()).Returns((PlayerType)1000);
            next.Setup(x => x.getColour()).Returns(PlayerColour.RED);
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            mockGame.SetupSequence(x => x.getWinner()).Throws(new NoWinnerException()).Returns(0);
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>());
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>
            {
                p.Object, next.Object
            });
            Assert.IsTrue(result.cur["name"].ToObject<string>() == "name");
        }

        [TestMethod]
        public void WillPopulateThePlayersArrayWithJObjectRepresentationsOfThePlayers()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>(MockBehavior.Loose);
            Mock<Player> p = new Mock<Player>();
            p.Setup(x => x.makeMove(mockGame.Object)).Returns(new Move());
            p.Setup(x => x.getPlayerType()).Returns(PlayerType.RANDOM);
            p.Setup(x => x.getName()).Returns("name");
            Mock<Player> next = new Mock<Player>();
            next.Setup(x => x.getName()).Returns("name");
            next.Setup(x => x.getPlayerType()).Returns(PlayerType.RANDOM);
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            mockGame.SetupSequence(x => x.getWinner()).Throws(new NoWinnerException()).Returns(0);
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>());
            BoardGameDTO result = service.processMove(mockGame.Object, p.Object, new List<Player>
            {
                p.Object, next.Object
            });
            Assert.IsTrue(result.players.Count == 2);
        }

        [TestMethod]
        public void WillReturnTheCurPlayerAsTheNextPlayerIfItIsAHumanPlayer()
        {
            Mock<Player> mockHuman = new Mock<Player>();
            mockHuman.Setup(x => x.getPlayerType()).Returns(PlayerType.HUMAN);
            mockHuman.Setup(x => x.getName()).Returns("name");
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>());
            BoardGameDTO result = service.processMove(mockGame.Object, mockHuman.Object, new List<Player>());
            Assert.AreEqual(PlayerType.HUMAN, result.cur["type"].ToObject<PlayerType>());
        }

        [TestMethod]
        public void WillNotCallTheHumanPlayerToMakeAMove()
        {
            Mock<Player> mockHuman = new Mock<Player>();
            mockHuman.Setup(x => x.getPlayerType()).Returns(PlayerType.HUMAN);
            mockHuman.Setup(x => x.getName()).Returns("name");
            mockHuman.Setup(x => x.makeMove(It.IsAny<BoardGame>()))
                .Returns((Move)null)
                .Verifiable();
            Mock<BoardGame> mockgame = new Mock<BoardGame>();
            mockgame.Setup(x => x.getBoard())
                .Returns(new List<List<BoardGame>>());
            service.processMove(mockgame.Object, mockHuman.Object, new List<Player>());
            mockHuman.Verify(x => x.makeMove(It.IsAny<BoardGame>()), Times.Never);
        }

        [TestMethod]
        public void WillAddTheAvailableMovesToTheDto()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getBoard())
                .Returns(new List<List<BoardGame>>());
            List<Move> available = new List<Move>();
            mockGame.Setup(x => x.getAvailableMoves()).Returns(available);
            Mock<Player> mockHuman = new Mock<Player>();
            mockHuman.Setup(x => x.getPlayerType()).Returns(PlayerType.HUMAN);
            mockHuman.Setup(x => x.getName()).Returns("name");
            mockHuman.Setup(x => x.makeMove(It.IsAny<BoardGame>()))
                .Returns((Move)null)
                .Verifiable();
            BoardGameDTO result = service.processMove(mockGame.Object, mockHuman.Object, new List<Player>());
            Assert.AreSame(available, result.availableMoves);
        }

        [TestMethod]
        public void WillSetTheMoveMadeByTheAiAsTheLastMove()
        {
            Mock<Player> mockAi = new Mock<Player>();
            Move m = new Move();
            mockAi.Setup(x => x.makeMove(It.IsAny<BoardGame>())).Returns(m);
            mockAi.Setup(x => x.getPlayerType()).Returns(PlayerType.RANDOM);
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockGame.Setup(x => x.getWinner()).Throws(new NoWinnerException());
            mockGame.Setup(x => x.getBoard()).Returns(new List<List<BoardGame>>());
            BoardGameDTO result = service.processMove(mockGame.Object, mockAi.Object, new List<Player>());
            Assert.AreSame(m, result.lastMove);
        }

        [TestMethod]
        public void WillCallTheNodeServiceToProcessTheBoard()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Mock<NodeService> mockService = new Mock<NodeService>();
            mockService.Setup(x => x.process(mockGame.Object, It.IsAny<PlayerColour>()))
                .Returns(new List<INode>())
                .Verifiable();
            service = new GameService(mockService.Object);
            service.rateMove(mockGame.Object, new Move { owner = PlayerColour.BLUE}, 0);
            mockService.Verify();
        }

        [TestMethod]
        public void WillCallTheNodeServiceWithTheColourOfTheOwnerOnTheMove()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Mock<NodeService> mockService = new Mock<NodeService>();
            Move move = new Move();
            move.owner = (PlayerColour)100;
            mockService.Setup(x => x.process(It.IsAny<BoardGame>(), (PlayerColour)100))
                .Returns(new List<INode>())
                .Verifiable();
            service = new GameService(mockService.Object);
            service.rateMove(mockGame.Object, move, 0);
            mockService.Verify();
        }

        [TestMethod]
        public void WillCallForTheMoveOfAllNodesReturnedByTheNodeService()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Mock<NodeService> mockService = new Mock<NodeService>();
            Move move = new Move();
            move.owner = (PlayerColour)100;
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.getMove())
                .Returns(move)
                .Verifiable();
            mockService.Setup(x => x.process(It.IsAny<BoardGame>(), (PlayerColour)100))
                .Returns(new List<INode>
                {
                    mockNode.Object,mockNode.Object,mockNode.Object,
                });
            service = new GameService(mockService.Object);
            service.rateMove(mockGame.Object, move, 0);
            mockNode.Verify(x => x.getMove(), Times.Exactly(3));
        }

        [TestMethod]
        public void WillReturnARatingDtoWithRating0IfValueOfNodeWithMatchingMoveIsMinusOne()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Mock<NodeService> mockService = new Mock<NodeService>();
            Move move = new Move();
            move.owner = (PlayerColour)100;
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.getMove())
                .Returns(move);
            mockNode.Setup(x => x.getReward())
                .Returns(-1);
            mockService.Setup(x => x.process(It.IsAny<BoardGame>(), (PlayerColour)100))
                .Returns(new List<INode>
                {
                    mockNode.Object
                });
            service = new GameService(mockService.Object);
            RatingDTO result = service.rateMove(mockGame.Object, move, 0);
            Assert.AreEqual(0, result.rating);
        }

        [TestMethod]
        public void WillReturnARatingDtoWithRating1IfValueOfNodeWithMatchingMoveIsOne()
        {
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            Mock<NodeService> mockService = new Mock<NodeService>();
            Move move = new Move();
            move.owner = (PlayerColour)100;
            Mock<INode> mockNode = new Mock<INode>();
            mockNode.Setup(x => x.getMove())
                .Returns(move);
            mockNode.Setup(x => x.getReward())
                .Returns(1);
            mockService.Setup(x => x.process(It.IsAny<BoardGame>(), (PlayerColour)100))
                .Returns(new List<INode>
                {
                    mockNode.Object
                });
            service = new GameService(mockService.Object);
            RatingDTO result = service.rateMove(mockGame.Object, move, 0);
            Assert.AreEqual(1, result.rating);
        }
    }
}
