using System;
using System.Collections.Generic;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class RandomAi : AbstractPlayer
    {

        public RandomAi(IRandomService random, IDatabaseProvider provider) : base(random, provider)
        {
            UserId = (int)type;
        }

        protected override INode decideMove(BoardGame game, List<INode> moves)
        {
            return moves[random.getRandomNumberBetween(0, moves.Count)];
        }
    }
}
