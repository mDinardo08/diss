using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class HumanPlayer : AbstractPlayer
    {
        public HumanPlayer(IRandomService random) : base(random)
        {
            type = PlayerType.HUMAN;
        }

        protected override Move decideMove(BoardGame game, List<Move> moves)
        {
            throw new NotImplementedException();
        }
    }
}
