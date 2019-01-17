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
            mockService.Setup(x => x.processMove(mockGame.Object, null)).Verifiable();
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
            mockGameService.Setup(x => x.processMove(mockGame.Object, mockPlayer.Object)).Returns((BoardGameDTO)null).Verifiable();
            cont = new GameController(mockGameService.Object, mockBoardService.Object, mockPlayerService.Object);
            cont.makeMove(new BoardGameDTO { cur = new JObject() });
            mockGameService.Verify();
        }
    }
}
