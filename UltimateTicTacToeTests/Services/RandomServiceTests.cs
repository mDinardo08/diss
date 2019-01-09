using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        WeylMiddleSquareService service;

        [TestInitialize()]
        public void Setup()
        {
            service = new WeylMiddleSquareService();
        }

        [TestMethod]
        public void WillReturn10SecondsFromEpoch()
        {
            Shim shim = Shim.Replace(() => DateTime.UtcNow).With(() => new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Utc));
            double result;
            PoseContext.Isolate(() => {
                result = service.GetSeed();
                Assert.IsTrue(result == 10);
            }, shim);
        }

        [TestMethod]
        public void WillReturn0IfTimeFromEpochIs0()
        {
            Shim shim = Shim.Replace(() => DateTime.UtcNow).With(() => new DateTime(1970,1,1,0,0,0, DateTimeKind.Utc));
            double result;
            PoseContext.Isolate(() => {
                result = service.GetSeed();
                Assert.IsTrue(result == 0);
            }, shim);
        }

        [TestMethod]
        public void WillReturn70IfNowIsAMinuteAnd10SecondsPastEpoch()
        {
            Shim shim = Shim.Replace(() => DateTime.UtcNow).With(() => new DateTime(1970, 1, 1, 0, 1, 10, DateTimeKind.Utc));
            double result;
            PoseContext.Isolate(() => {
                result = service.GetSeed();
                Assert.IsTrue(result == 70);
            }, shim);
        }

        [TestMethod]
        public void WillReturn10IfSecondsFromEpochIs10()
        {
            Shim shim = Shim.Replace(() => DateTime.UtcNow).With(() => new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Utc));
            double result;
            PoseContext.Isolate(() => {
                result = service.GetSeed();
                Assert.IsTrue(result == 10);
            }, shim);
        }

        [TestMethod]
        public void WillTrimTheMostAndLeastSignificantNosToProduceANoOfLength12()
        {
            long result = service.MiddleSquare(123456789876543210);
            Assert.AreEqual(456789876543, result);
        }

        [TestMethod]
        public void WillOnlyRemoveMostSigValueIfNoIs1OverLimit()
        {
            long result = service.MiddleSquare(1234567899876);
            Assert.AreEqual(234567899876, result);
        }

        [TestMethod]
        public void WillGenerate754501141766IfSeedIs1234567890()
        {

            Shim shim = Shim.Replace(() => DateTime.UtcNow).With(() => new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Utc).AddSeconds(1234567890));
            PoseContext.Isolate(() => {
                double result;
                result = service.getRandomNumber();
                Assert.IsTrue(result == 7545011417);
            }, shim);

        }
    }
}
