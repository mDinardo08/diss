using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;
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

        public List<INode> process(BoardGame game)
        {
            DateTime startTime = DateTime.UtcNow;
            TimeSpan duration = TimeSpan.FromSeconds(1.5);
            INode root = nodeService.createNode(game.Clone() as BoardGame);
            while(DateTime.UtcNow - startTime < duration)
            {
                expansion(traverse(root));
            }
            return root.getChildren();
        }

        public INode traverse(INode node)
        {
            INode cur = node;
            while (!cur.isLeaf())
            {
                List<INode> children = node.getChildren();
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
                node.getChildren()[0].rollOut();
            }
        }
    }
}
