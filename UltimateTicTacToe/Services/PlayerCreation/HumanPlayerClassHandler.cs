using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class HumanPlayerClassHandler : AbstractPlayerClassHandler
    {
        public HumanPlayerClassHandler(IRandomService randomService, NodeService nodeService) : base(randomService, nodeService)
        {
            type = PlayerType.HUMAN;
            successor = new MCTSPlayerClassHandler(randomService, nodeService);
        }

        protected override Player buildPlayer()
        {
            return new HumanPlayer(randomService);
        }
    }
}
