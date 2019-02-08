using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class MCTSPlayer : AbstractPlayer
    {
        private NodeService nodeService;

        public MCTSPlayer(IRandomService random) : base(random)
        { 
            type = PlayerType.MCTS;
        }

        protected override INode decideMove(BoardGame game, List<INode> nodes)
        {
            INode best = null;
            double max = Int32.MinValue;
            foreach (INode node in nodes)
            {
                if(node.getReward() > max)
                {
                    best = node;
                    max = node.getReward();
                }
            }
            return best;
        }
    }
}
