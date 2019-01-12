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
        public void WillReturnNullIfThereIfObjectPassedInIsNull()
        {
            Player player = service.createPlayer(null);
            Assert.IsNull(player);
        }

        [TestMethod]
        public void WillReturnARandomAIIfTypeIsRandom()
        {
            JObject obj = new JObject();
            obj.Add("type", JToken.FromObject(PlayerType.RANDOM));
            Player player = service.createPlayer(obj);
            Assert.IsTrue(player is RandomAi);
        }

        [TestMethod]
        public void WillPassARandomServiceToTheRandomAI()
        {
            Mock<IRandomService> mockService = new Mock<IRandomService>();
            service = new PlayerCreationService(mockService.Object);
            JObject obj = new JObject();
            obj.Add("type", JToken.FromObject(PlayerType.RANDOM));
            RandomAi player = service.createPlayer(obj) as RandomAi;
            Assert.AreEqual(mockService.Object, player.random);
        }

        [TestMethod]
        public void WillCreateARandomAiFromARandomPlayerType()
        {
            Player player = service.createPlayer(PlayerType.RANDOM);
            Assert.IsTrue(player is RandomAi);
        }

        [TestMethod]
        public void WillPassARandomServiceToTheRandomAi()
        {
            Mock<IRandomService> mockService = new Mock<IRandomService>();
            service = new PlayerCreationService(mockService.Object);
            RandomAi player = service.createPlayer(PlayerType.RANDOM) as RandomAi;
            Assert.IsNotNull(player.random);
        }
    }
}
