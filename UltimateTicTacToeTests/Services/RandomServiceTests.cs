using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pose;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Services
{
    [TestClass]
    public class RandomServiceTests
    {
        RandomService service;

        [TestInitialize()]
        public void Setup()
        {
            service = new RandomService();
        }

        [TestMethod]
        public void WillReturnWhatTheRandomObjectReturns()
        {
            Mock<Random> mock = new Mock<Random>(MockBehavior.Strict);
            mock.Setup(x => x.Next()).Returns(0);
            service.random = mock.Object;
            Assert.AreEqual(0, service.getRandomNumber());
        }

        [TestMethod]
        public void WillReturnTheIntegerTheRandomObjectReturns()
        {
            Mock<Random> mock = new Mock<Random>(MockBehavior.Strict);
            mock.Setup(x => x.Next()).Returns(1234567890);
            service.random = mock.Object;
            Assert.AreEqual(1234567890, service.getRandomNumber());
        }

        [TestMethod]
        public void WillPassArguementsToRandomObject()
        {
            int low = 0;
            int high = 1;
            Mock<Random> mock = new Mock<Random>(MockBehavior.Strict);
            mock.Setup(x => x.Next(low, high)).Returns(0);
            service.random = mock.Object;
            service.getRandomNummberBetween(low, high);
            mock.Verify(m => m.Next(low, high), Times.Once);
        }

        [TestMethod]
        public void WillReturnWhatRandomReturns()
        {
            int low = 0;
            int high = 1;
            Mock<Random> mock = new Mock<Random>(MockBehavior.Strict);
            mock.Setup(x => x.Next(low, high)).Returns(1234567890);
            service.random = mock.Object;
            var result = service.getRandomNummberBetween(low, high);
            Assert.AreEqual(1234567890, result);
        }
    }
}
