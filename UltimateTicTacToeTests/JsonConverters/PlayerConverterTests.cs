using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using UltimateTicTacToe.JsonConverters.AiPlayer;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToeTests.JsonConverters
{
    [TestClass]
    public class PlayerConverterTests
    {
        PlayerConverter conv;

        [TestInitialize()]
        public void Setup()
        {
            conv = new PlayerConverter();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WillThrowInvalidOpertationExceptionOnWriteJson()
        {
            conv.WriteJson(null, null, null); 
        }

        [TestMethod]
        public void WillNotConvertString()
        {
            Assert.IsFalse(conv.CanConvert(typeof(string)));
        }

        [TestMethod]
        public void WillConvertRandomAiPlayer()
        {
            Player result = JsonConvert.DeserializeObject<Player>("{\"type\": 0}");
            Assert.IsTrue(result is RandomAi);
        }
    }
}
