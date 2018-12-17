using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateTicTacToe.JsonConverters;
using UltimateTicTacToe.JsonConverters.Board;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToeTests.JsonConverters
{
    [TestClass]
    public class BoardConverterTests
    {

        BoardConverter conv;

        [TestInitialize()]
        public void Setup()
        {
            conv = new BoardConverter();
        }

        [TestMethod]
        public void WillReturnFalseIfTypeIsNotOfTypeBoardGame()
        {
            Assert.IsFalse(conv.CanConvert(typeof(string)));
        }

        [TestMethod]
        public void WillReturnTrueIfTypeIsOfTypeBoardGame()
        {
            Assert.IsTrue(conv.CanConvert(typeof(TicTacToe)));
        }

        [TestMethod]
        public void WillReturnTrueForAllTypesOfBoardGame()
        {
            Assert.IsTrue(conv.CanConvert(typeof(Tile)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WillThrowInvalidOpertationExceptionIfWritingJSON()
        {
            conv.WriteJson(null, null, null);
        }

        [TestMethod]
        public void WillCorrectlyConvertTicTacToeFromBoardGame()
        {
            var json = "{\"board\": [[]], \"winChecker\": {}}";
            var result = JsonConvert.DeserializeObject<BoardGame>(json);
            Assert.IsTrue(result is TicTacToe);
        }

        [TestMethod]
        public void WillCorrectlyConvertToTileFromBoardGame()
        {
            Tile game = new Tile();
            var json = JsonConvert.SerializeObject(game);
            var result = JsonConvert.DeserializeObject<BoardGame>(json);
            Assert.IsTrue(result is Tile);
        }

        [TestMethod]
        public void WillCorrectlyPopulatePropertiesOfTile()
        {
            Tile tile = new Tile();
            tile.makeMove(new Move { owner = new Player{ name = "test" } });
            var json = JsonConvert.SerializeObject(tile);
            Tile result = JsonConvert.DeserializeObject<BoardGame>(json) as Tile;
            Assert.AreEqual(tile.owner.name, result.owner.name);
        }
    }
}
