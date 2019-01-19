using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToeTests.Models
{
    [TestClass]
    public class HumanPlayerTests
    {

        [TestMethod]
        public void WillReturnPlayerTypeHuman()
        {
            Player player = new HumanPlayer(null);
            Assert.AreEqual(PlayerType.HUMAN, player.getPlayerType());
        }
    }
}
