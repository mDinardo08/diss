using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Models.MCTS
{
    public class Node : AbstractNode
    {
        private BoardGame game;

        public Node(BoardGame game)
        {
            this.game = game;
        }

        public override BoardGame BoarrollOutState(BoardGame gameState)
        {
            throw new NotImplementedException();
        }
    }
}
