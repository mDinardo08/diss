using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.JsonConverters.WinCheck;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToeTests.JsonConverters
{
    [TestClass]
    public class WinCheckConverterTests
    {
        WinCheckConverter conv;

        [TestInitialize()]
        public void Setup()
        {
            conv = new WinCheckConverter();
        }

        [TestMethod]
        public void WillReturnFalseIfTypeIsNotWinChecker()
        {
            bool result = conv.CanConvert(typeof(int));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WillReturnTrueIfTypeIsWinChecker()
        {
            bool result = conv.CanConvert(typeof(HorizontalWinChecker));
            Assert.IsTrue(result);
        }
    }
}
