using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Models.MCTS
{
    public abstract class AbstractNode : INode
    {
        public BoardGame subject;
        public bool expanded;
        private int noVists;
        private int totalScore;
        public void expand()
        {
            throw new NotImplementedException();
        }

        public List<INode> getChildren()
        {
            throw new NotImplementedException();
        }

        public INode getParent()
        {
            throw new NotImplementedException();
        }

        public bool isExpanded()
        {
            throw new NotImplementedException();
        }

        public bool isLeaf()
        {
            throw new NotImplementedException();
        }

        public void rollOut()
        {
            throw new NotImplementedException();
        }

        public void setParent(INode parent)
        {
            throw new NotImplementedException();
        }

        public int getVisits()
        {
            throw new NotImplementedException();
        }

        public int getTotalScore()
        {
            throw new NotImplementedException();
        }

        public abstract BoardGame BoarrollOutState(BoardGame gameState);

        public double getUBC1()
        {
            throw new NotImplementedException();
        }
    }
}
