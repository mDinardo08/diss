using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.MCTS
{
    public interface INode
    {
        void expand();
        List<INode> getChildren();
        INode getParent();
        bool isExpanded();
        bool isLeaf();
        void rollOut();
        int getVisits();
        int getTotalScore();
        double getUBC1();
    }
}
