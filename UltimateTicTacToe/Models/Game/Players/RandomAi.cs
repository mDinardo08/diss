using System;
using System.Collections.Generic;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class RandomAi : AbstractPlayer
    {

        public RandomAi(IRandomService random) : base(random)
        {
        }

        protected override INode decideMove(BoardGame game, List<INode> moves)
        {
            return moves[random.getRandomNumberBetween(0, moves.Count)];
        }
    }
}
