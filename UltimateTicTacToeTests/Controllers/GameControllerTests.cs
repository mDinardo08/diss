using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UltimateTicTacToe.Controllers;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Controllers
{
    [TestClass]
    public class GameControllerTests
    {
        GameController cont;
        Mock<IGameService> mockService;

        [TestMethod]
        public void WillPassTheGameToTheBoardGameCreationService()
        {
            BoardGameDTO dto = new BoardGameDTO();
            List<List<JObject>> obj = new List<List<JObject>>
            {
                new List<JObject>
                {
                    new JObject()
                }
            };
            Mock<BoardCreationService> mockService = new Mock<BoardCreationService>(MockBehavior.Loose);
            mockService.Setup(x => x.createBoardGame(dto)).Verifiable();
            cont = new GameController(new Mock<IGameService>(MockBehavior.Loose).Object, mockService.Object,  new Mock<IPlayerCreationService>().Object);
            cont.makeMove(dto);
            mockService.Verify();
        }

        [TestMethod]
        public void WillPassTheGameReturnedFromBoardCreationToGameService()
        {
            Mock<BoardCreationService> mockCreationService = new Mock<BoardCreationService>();
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockCreationService.Setup(x => x.createBoardGame(It.IsAny<BoardGameDTO>())).Returns(mockGame.Object);
            Mock<IGameService> mockService = new Mock<IGameService>();
            mockService.Setup(x => x.processMove(mockGame.Object, null, null)).Verifiable();
            cont = new GameController(mockService.Object, mockCreationService.Object, new Mock<IPlayerCreationService>().Object);
            cont.makeMove(new BoardGameDTO());
            mockService.Verify();
        }

        [TestMethod]
        public void WillPassReturnedBoardAndPlayerToGameService()
        {
            Mock<IPlayerCreationService> mockPlayerService = new Mock<IPlayerCreationService>(MockBehavior.Loose);
            Mock<BoardCreationService> mockBoardService = new Mock<BoardCreationService>(MockBehavior.Loose);
            Mock<IGameService> mockGameService = new Mock<IGameService>(MockBehavior.Strict);
            Mock<Player> mockPlayer = new Mock<Player>();
            Mock<BoardGame> mockGame = new Mock<BoardGame>();
            mockPlayerService.Setup(x => x.createPlayer(It.IsAny<JObject>())).Returns(mockPlayer.Object);
            mockBoardService.Setup(x => x.createBoardGame(It.IsAny<BoardGameDTO>())).Returns(mockGame.Object);
            mockGameService.Setup(x => x.processMove(mockGame.Object, mockPlayer.Object, null)).Returns((BoardGameDTO)null).Verifiable();
            cont = new GameController(mockGameService.Object, mockBoardService.Object, mockPlayerService.Object);
            cont.makeMove(new BoardGameDTO { cur = new JObject() });
            mockGameService.Verify();
        }

        [TestMethod]
        public void WillPassTheSizeFromTheCreationDtoToTheBoardCreation()
        {
            BoardCreationDto boardCreationDto = new BoardCreationDto();
            boardCreationDto.size = 3;
            Mock<IPlayerCreationService> mockPlayerService = new Mock<IPlayerCreationService>();
            mockPlayerService.Setup(x => x.createPlayers(It.IsAny<List<JObject>>()))
                .Returns(new List<Player>
                {
                    new Mock<Player>().Object
                });
            Mock<BoardCreationService> mockCreationService = new Mock<BoardCreationService>(MockBehavior.Strict);
            mockCreationService.Setup(x => x.createBoardGame(3)).Returns((BoardGame)null).Verifiable();
            cont = new GameController(new Mock<IGameService>().Object, mockCreationService.Object, mockPlayerService.Object);
            cont.createBoard(boardCreationDto);
            mockCreationService.Verify();
        }

        [TestMethod]
        public void WillPassThePlayersFromThePlayerArrayToThePlayerCreationService()
        {
            BoardCreationDto boardCreationDto = new BoardCreationDto();
            boardCreationDto.players = new List<JObject>
            {
                new JObject()
            };
            Mock<BoardCreationService> mockCreationService = new Mock<BoardCreationService>();
            Mock<IPlayerCreationService> mockPlayerService = new Mock<IPlayerCreationService>(MockBehavior.Strict);
            mockPlayerService.Setup(x => x.createPlayers(boardCreationDto.players))
                .Returns(new List<Player>
                {
                    new Mock<Player>().Object
                })
                .Verifiable();
            cont = new GameController(new Mock<IGameService>().Object, mockCreationService.Object, mockPlayerService.Object);
            cont.createBoard(boardCreationDto);
            mockPlayerService.Verify();
        }

        [TestMethod]
        public void WillPassTheFirstPlayerReturnedAsTheCurrentPlayerToTheGameService()
        {
            Mock<IGameService> mockGameServce = new Mock<IGameService>(MockBehavior.Strict);
            Mock<BoardCreationService> mockCreationService = new Mock<BoardCreationService>();
            Mock<IPlayerCreationService> mockPlayerService = new Mock<IPlayerCreationService>();
            Mock<Player> mockPlayer = new Mock<Player>();
            mockPlayerService.Setup(x => x.createPlayers(It.IsAny<List<JObject>>()))
                .Returns(new List<Player>
                {
                    mockPlayer.Object, null
                });
            mockGameServce.Setup(x => x.processMove(null, mockPlayer.Object, It.IsAny<List<Player>>()))
                .Returns((BoardGameDTO)null)
                .Verifiable();
            BoardCreationDto creationDto = new BoardCreationDto();
            cont = new GameController(mockGameServce.Object, mockCreationService.Object, mockPlayerService.Object);
            cont.createBoard(creationDto);
            mockGameServce.Verify();
        }

        [TestMethod]
        public void WillPassAllPlayersToTheGameService()
        {
            Mock<IGameService> mockGameServce = new Mock<IGameService>(MockBehavior.Strict);
            Mock<BoardCreationService> mockCreationService = new Mock<BoardCreationService>();
            Mock<IPlayerCreationService> mockPlayerService = new Mock<IPlayerCreationService>();
            Mock<Player> mockPlayer = new Mock<Player>();
            List<Player> players = new List<Player>
            {
                mockPlayer.Object, mockPlayer.Object
            };
            mockPlayerService.Setup(x => x.createPlayers(It.IsAny<List<JObject>>()))
                .Returns(players);
            mockGameServce.Setup(x => x.processMove(null, mockPlayer.Object, players))
                .Returns((BoardGameDTO)null)
                .Verifiable();
            BoardCreationDto creationDto = new BoardCreationDto();
            cont = new GameController(mockGameServce.Object, mockCreationService.Object, mockPlayerService.Object);
            cont.createBoard(creationDto);
            mockGameServce.Verify();
        }

        [TestMethod]
        public void WillCallPlayerCreationServiceToCreatePlayers()
        {
            BoardGameDTO dto = new BoardGameDTO();
            Mock<Player> mockPlayer = new Mock<Player>();
            dto.players = new List<JObject>
            {
                new JObject()
            };
            Mock<IGameService> gameService = new Mock<IGameService>();
            Mock<BoardCreationService> boardCreationService = new Mock<BoardCreationService>();
            Mock<IPlayerCreationService> playerCreationService = new Mock<IPlayerCreationService>();
            playerCreationService.Setup(x => x.createPlayers(dto.players))
                .Returns(new List<Player>())
                .Verifiable();
            cont = new GameController(gameService.Object, boardCreationService.Object, playerCreationService.Object);
            cont.makeMove(dto);
            playerCreationService.Verify();
        }

        public void WillPassThePlayersArrayToTheGameService()
        {
            BoardGameDTO dto = new BoardGameDTO();
            Mock<Player> mockPlayer = new Mock<Player>();
            dto.players = new List<JObject>
            {
                new JObject()
            };
            List<Player> players = new List<Player>
            {
                mockPlayer.Object, mockPlayer.Object
            };
            Mock<IGameService> gameService = new Mock<IGameService>();
            Mock<BoardCreationService> boardCreationService = new Mock<BoardCreationService>();
            Mock<IPlayerCreationService> playerCreationService = new Mock<IPlayerCreationService>();
            playerCreationService.Setup(x => x.createPlayers(dto.players))
                .Returns(players);
            gameService.Setup(x => x.processMove(It.IsAny<BoardGame>(), It.IsAny<Player>(), players))
                .Returns((BoardGameDTO)null)
                .Verifiable();
            cont = new GameController(gameService.Object, boardCreationService.Object, playerCreationService.Object);
            cont.makeMove(dto);
            gameService.Verify();
        }
    }
}
