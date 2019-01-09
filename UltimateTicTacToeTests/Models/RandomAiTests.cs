
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class RandomAiTests
    {
        RandomAi player;

        [TestInitialize()]
        public void Setup()
        {
            player = new RandomAi();
        }

        [TestMethod]
        public void WillRetrunWhateverNameItWasInitialisedWith()
        {
            player.name = "I am so random XD !!";
            Assert.IsTrue(String.Equals("I am so random XD !!", player.getName()));
        }


    }
}
