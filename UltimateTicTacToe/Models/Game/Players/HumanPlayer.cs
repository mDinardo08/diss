using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class HumanPlayer : AbstractPlayer
    {
        public HumanPlayer(IRandomService random) : base(random)
        {
            type = PlayerType.HUMAN;
        }

        protected override INode decideMove(BoardGame game, List<INode> moves)
        {
            throw new NotImplementedException();
        }
    }
}
