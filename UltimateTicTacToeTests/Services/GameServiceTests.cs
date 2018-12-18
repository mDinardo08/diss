using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UltimateTicTacToe.Models;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToeTests.Services
{
    [TestClass]
    public class GameServiceTests
    {
        GameService service;

        [TestInitialize()]
        public void Setup()
        {
            service = new GameService();
        }

        
    }
}
