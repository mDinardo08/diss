using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class HumanPlayer : AbstractPlayer
    {
        public HumanPlayer(IRandomService random, IDatabaseProvider provider) : base(random, provider)
        {
            type = PlayerType.HUMAN;
        }

        protected override INode decideMove(BoardGame game, List<INode> moves, int opponentId)
        {
            throw new NotImplementedException();
        }
    }
}
