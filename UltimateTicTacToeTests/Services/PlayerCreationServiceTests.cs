using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
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
            service = new PlayerCreationService();
        }


    }
}
