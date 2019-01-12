using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Services
{
    [TestClass]
    public class PlayerCreationServiceTests
    {
        IPlayerCreationService service;

        [TestInitialize()]
        public void Setup()
        {
            service = new PlayerCreationService(null);
        }
        
        [TestMethod]
        public void WillPassPlayerTypeToClassHandler()
        {
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>(MockBehavior.Strict);
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000)).Returns((Player)null).Verifiable();
            service = new PlayerCreationService(mockHandler.Object);
            service.createPlayer((PlayerType)1000);
            mockHandler.Verify();
        }

        [TestMethod]
        public void WillReturnThePlayerReturnedFromTheHandler()
        {
            Mock<Player> mockPlayer = new Mock<Player>(); 
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>(MockBehavior.Strict);
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000)).Returns(mockPlayer.Object).Verifiable();
            service = new PlayerCreationService(mockHandler.Object);
            Player result = service.createPlayer((PlayerType)1000);
            Assert.AreEqual(mockPlayer.Object, result);
        }

        [TestMethod]
        public void WillCallPlayerHandlerWithTheTypeFromTheJObject()
        {
            JObject player = new JObject();
            player.Add("type", 1000);
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>(MockBehavior.Strict);
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000)).Returns((Player)null).Verifiable();
            service = new PlayerCreationService(mockHandler.Object);
            service.createPlayer(player);
            mockHandler.Verify();
        }

        [TestMethod]
        public void WillReturnThePlayerReturnedFromTheHandlerFromAJObject()
        {
            JObject player = new JObject();
            player.Add("type", 1000);
            Mock<Player> mockPlayer = new Mock<Player>();
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>(MockBehavior.Strict);
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000)).Returns(mockPlayer.Object).Verifiable();
            service = new PlayerCreationService(mockHandler.Object);
            Player result = service.createPlayer(player);
            Assert.AreEqual(mockPlayer.Object, result);
        }

        [TestMethod]
        public void WillReturnNullIfNullIsPassedIn()
        {
            Player result = service.createPlayer(null);
            Assert.IsNull(result);
        }
    }
}
