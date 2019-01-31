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

        public List<INode> process(BoardGame game)
        {
            DateTime startTime = DateTime.UtcNow;
            TimeSpan duration = TimeSpan.FromSeconds(1.5);
            INode root = new Node(game, null, null);
            while(DateTime.UtcNow - startTime < duration)
            {
                rollout(traverse(root));
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

        public void rollout(INode node)
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
