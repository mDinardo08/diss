using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Models.MCTS
{
    public class Node : INode
    {

        public BoardGame subject;
        public bool expanded;
        private int noVists;
        private int totalScore;
        private INode parent;
        private Move move;
        public Node(BoardGame game, INode parent, Move move)
        {
            this.parent = parent;
            subject = game;
            this.move = move;
            expanded = false;
        }

        public void expand()
        {
            expanded = true;
        }

        public List<INode> getChildren()
        {
            throw new NotImplementedException();
        }

        public INode getParent()
        {
            return parent;
        }

        public int getTotalScore()
        {
            throw new NotImplementedException();
        }

        public double getUBC1()
        {
            throw new NotImplementedException();
        }

        public int getVisits()
        {
            throw new NotImplementedException();
        }

        public bool isExpanded()
        {
            throw new NotImplementedException();
        }

        public bool isLeaf()
        {
            return !expanded; 
        }

        public void rollOut()
        {
            throw new NotImplementedException();
        }
    }
}
