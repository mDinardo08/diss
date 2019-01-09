using System;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class RandomAi : Player
    {
        public AiPlayerType type = AiPlayerType.RANDOM;
        public string name;
        private IRandomService randomService;

        public RandomAi(IRandomService randomService)
        {
            this.randomService = randomService;
        }

        public string getName()
        {
            return name;
        }

        public BoardGame makeMove(BoardGame game)
        {
            int index = randomService.getRandomNummberBetween(0, game.getAvailableMoves().Count);
            Move move = game.getAvailableMoves()[index];
            move.setOwner(this);
            game.makeMove(move);
            return game;
        }
    }
}
