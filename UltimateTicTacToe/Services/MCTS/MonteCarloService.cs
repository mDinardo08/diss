using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.MCTS;

namespace UltimateTicTacToe.Services
{
    public class MonteCarloService : NodeService
    {
        public INode root;
        private INodeCreationService nodeService;

        public MonteCarloService(INodeCreationService nodeService)
        {
            this.nodeService = nodeService;
        }

        public List<INode> process(BoardGame game, PlayerColour colour)
        {
            DateTime startTime = DateTime.UtcNow;
            TimeSpan duration = TimeSpan.FromSeconds(2);
            INode root = nodeService.createNode(game.Clone() as BoardGame, colour);
            root.expand();
            for(int i = 0; i < 3000; i++)
            {
                expansion(traverse(root));
            }
            return root.getChildren();
        }

        public INode traverse(INode node)
        {
            INode cur = node;
            int depth = 0;
            INode previous = cur;
            while (!cur.isLeaf())
            {
                previous = cur;
                depth++;
                List<INode> children = cur.getChildren();
                double max = Int64.MinValue;
                foreach (INode child in children)
                {
                    double ucb1 = child.getUBC1();
                    if (ucb1 > max)
                    {
                        cur = child;
                        max = ucb1;
                    }
                }
            }
            return cur;
        }

        public void expansion(INode node)
        {
            if (node.getVisits() == 0)
            {
                node.rollOut();
            } else
            {
                node.expand();
                if (node.getChildren().Count > 0)
                {
                    node.getChildren()[0].rollOut();
                }else
                {
                    node.rollOut();
                }
            }
        }
    }
}
