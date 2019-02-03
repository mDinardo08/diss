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

        public MCTSPlayer(IRandomService random, NodeService nodeService) : base(random)
        {
            this.nodeService = nodeService;
            type = PlayerType.MCTS;
        }

        protected override Move decideMove(BoardGame game, List<Move> moves)
        {
            List<INode> nodes = nodeService.process(game, this.getColour());
            Move move = new Move();
            double max = Int32.MinValue;
            foreach (INode node in nodes)
            {
                if(node.getReward() > max)
                {
                    move = node.getMove();
                    max = node.getReward();
                }
            }
            Console.WriteLine("Max node value: {0}", max);
            return move;
        }
    }
}
