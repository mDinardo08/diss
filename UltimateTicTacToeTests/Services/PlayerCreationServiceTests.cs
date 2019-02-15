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
            player.Add("name", "");
            player.Add("colour", 1000);
            player.Add("userId", 0);
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>(MockBehavior.Strict);
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000))
                .Returns(new Mock<Player>().Object)
                .Verifiable();
            service = new PlayerCreationService(mockHandler.Object);
            service.createPlayer(player);
            mockHandler.Verify();
        }

        [TestMethod]
        public void WillReturnThePlayerReturnedFromTheHandlerFromAJObject()
        {
            JObject player = new JObject();
            player.Add("type", 1000);
            player.Add("name", "");
            player.Add("colour", 1000);
            player.Add("userId", 0);
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

        [TestMethod]
        public void WillCallHandlerForEveryJObjectInTheArray()
        {
            List<JObject> jObjects = new List<JObject>();
            JObject jObj = new JObject();
            jObj.Add("type", 1000);
            jObj.Add("name", "");
            jObj.Add("colour", 100);
            jObj.Add("userId", 0);
            jObjects.Add(jObj);
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>();
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000))
                .Returns(new Mock<Player>().Object)
                .Verifiable();
            service = new PlayerCreationService(mockHandler.Object);
            service.createPlayers(jObjects);
            mockHandler.Verify();
        }

        [TestMethod]
        public void WIllCallTheHandlerOncePerJObject()
        {
            List<JObject> jObjects = new List<JObject>();
            JObject jObj = new JObject();
            jObj.Add("type", 1000);
            jObj.Add("name", "");
            jObj.Add("colour", 100);
            jObj.Add("userId", 0);
            jObjects.Add(jObj);
            jObjects.Add(jObj);
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>();
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000))
                .Returns(new Mock<Player>().Object)
                .Verifiable();
            service = new PlayerCreationService(mockHandler.Object);
            service.createPlayers(jObjects);
            Assert.AreEqual(2, mockHandler.Invocations.Count);
        }

        [TestMethod]
        public void WillSetTheNameOfThePlayer()
        {
            JObject player = new JObject();
            player.Add("type", 1000);
            player.Add("name", "some silly name");
            player.Add("colour", 100);
            player.Add("userId", 0);
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>();
            Mock<Player> mockPlayer = new Mock<Player>(MockBehavior.Loose);
            mockPlayer.Setup(x => x.setName("some silly name")).Verifiable();
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000)).Returns(mockPlayer.Object);
            service = new PlayerCreationService(mockHandler.Object);
            service.createPlayer(player);
            mockPlayer.Verify();
        }

        [TestMethod]
        public void WillSetTheNamesOfThePlayersInTheArray()
        {
            JObject player = new JObject();
            player.Add("type", 1000);
            player.Add("name", "some silly name");
            player.Add("colour", 10);
            player.Add("userId", 0);
            List<JObject> players = new List<JObject>
            {
                player
            };
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>();
            Mock<Player> mockPlayer = new Mock<Player>(MockBehavior.Loose);
            mockPlayer.Setup(x => x.setName("some silly name")).Verifiable();
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000)).Returns(mockPlayer.Object);
            service = new PlayerCreationService(mockHandler.Object);
            service.createPlayers(players);
            mockPlayer.Verify();
        }

        [TestMethod]
        public void WillReturnAnArrayOfWhatWasReturnedByTheHandler()
        {
            JObject player = new JObject();
            player.Add("type", 1000);
            player.Add("name", "");
            player.Add("colour", 100);
            player.Add("userId", 0);
            List<JObject> players = new List<JObject>
            {
                player
            };
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>();
            Mock<Player> mockPlayer = new Mock<Player>();
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000))
                .Returns(mockPlayer.Object);
            service = new PlayerCreationService(mockHandler.Object);
            List<Player> result = service.createPlayers(players);
            Assert.IsTrue(result[0] == mockPlayer.Object);
        }

        [TestMethod]
        public void WillSetThePlayerColourField()
        {
            JObject player = new JObject();
            player.Add("type", 1000);
            player.Add("colour", 1000);
            player.Add("name", "some silly name");
            player.Add("userId", 0);
            Mock<PlayerClassHandler> mockHandler = new Mock<PlayerClassHandler>();
            Mock<Player> mockPlayer = new Mock<Player>(MockBehavior.Strict);
            mockPlayer.Setup(x => x.setName("some silly name"));
            mockPlayer.Setup(x => x.setColour((PlayerColour)1000)).Verifiable();
            mockPlayer.Setup(x => x.setUserId(It.IsAny<int>()));
            mockHandler.Setup(x => x.createPlayer((PlayerType)1000)).Returns(mockPlayer.Object);
            service = new PlayerCreationService(mockHandler.Object);
            service.createPlayer(player);
            mockPlayer.Verify();
        }
    }
}
