using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Services
{
    [TestClass]
    public class RandomPlayerClassHandlerTest
    {
        PlayerClassHandler hand;

        [TestInitialize()]
        public void Setup()
        {
            hand = new RandomPlayerClassHandler(null, null);
        }

        [TestMethod]
        public void WillReturnARandomAiIfTypeIsRandom()
        {
            Player player = hand.createPlayer(PlayerType.RANDOM);
            Assert.IsTrue(player is RandomAi);
        }

        [TestMethod]
        public void WillPassRandomServiceToAi()
        {
            Mock<IRandomService> mockService = new Mock<IRandomService>();
            hand = new RandomPlayerClassHandler(mockService.Object, null);
            RandomAi player = hand.createPlayer(PlayerType.RANDOM) as RandomAi;
            Assert.IsNotNull(player.random);
        }

        [TestMethod]
        public void TypeWillBeRandom()
        {
            Assert.AreEqual((hand as RandomPlayerClassHandler).type, PlayerType.RANDOM);
        }

        [TestMethod]
        public void WillCallSuccessorIfGivenTypeDoesNotMatchType()
        {
            Mock<PlayerClassHandler> mockHand = new Mock<PlayerClassHandler>(MockBehavior.Strict);
            mockHand.Setup(x => x.createPlayer((PlayerType) 1000)).Returns((Player)null).Verifiable();
            (hand as RandomPlayerClassHandler).successor = mockHand.Object;
            hand.createPlayer((PlayerType)1000);
            mockHand.Verify();
        }

        [TestMethod]
        public void WillReturnWhateverPlayerTheSuccessorReturns()
        {
            Mock<PlayerClassHandler> mockHand = new Mock<PlayerClassHandler>(MockBehavior.Strict);
            Mock<Player> mockPlayer = new Mock<Player>();
            mockHand.Setup(x => x.createPlayer((PlayerType)1000)).Returns(mockPlayer.Object);
            (hand as RandomPlayerClassHandler).successor = mockHand.Object;
            Player result = hand.createPlayer((PlayerType)1000);
            Assert.AreEqual(mockPlayer.Object, result);
        }

        [TestMethod]
        public void WillReturnNullIfThereIsNoSuccessorAndTypeDoesNotMatch()
        {
            Player result = hand.createPlayer((PlayerType)1000);
            Assert.IsNull(result);
        }
    }
}
