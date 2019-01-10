using System;
using System.Collections.Generic;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class RandomAi : AbstractPlayer
    {
        public PlayerType type = PlayerType.RANDOM;

        public RandomAi(IRandomService random) : base(random)
        {
        }

        protected override Move decideMove(BoardGame game, List<Move> moves)
        {
            return moves[random.getRandomNumberBetween(0, moves.Count)];
        }
    }
}
