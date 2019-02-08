using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class MCTSPlayerClassHandler : AbstractPlayerClassHandler
    {

        public MCTSPlayerClassHandler(IRandomService randomService, NodeService nodeService) : base(randomService, nodeService)
        {
            this.nodeService = nodeService;
            type = PlayerType.MCTS;
        }

        protected override Player buildPlayer()
        {
            return new MCTSPlayer(randomService);
        }
    }
}
