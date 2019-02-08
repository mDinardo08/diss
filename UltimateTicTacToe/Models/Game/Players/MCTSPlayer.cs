using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class MCTSPlayer : AbstractPlayer
    {

        public MCTSPlayer(IRandomService random, IDatabaseProvider provider) : base(random, provider)
        { 
            type = PlayerType.MCTS;
            UserId = (int)type;
        }

        protected override INode decideMove(BoardGame game, List<INode> nodes, int opponentId)
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
