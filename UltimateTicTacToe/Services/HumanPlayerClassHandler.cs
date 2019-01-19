using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class HumanPlayerClassHandler : AbstractPlayerClassHandler
    {
        public HumanPlayerClassHandler(IRandomService randomService) : base(randomService)
        {
            type = PlayerType.HUMAN;
            player = new HumanPlayer(randomService);
        }
    }
}
