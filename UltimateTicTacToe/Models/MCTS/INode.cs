using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Models.MCTS
{
    public interface INode
    {
        bool isLeaf();
        List<INode> getChildren();
        double getUBC1();
        int getVisits();
        void rollOut();
        void expand();
        INode getParent();
        void backPropagate(int score);
        double getReward();
        Move getMove();
    }
}
