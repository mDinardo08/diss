using System;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class RandomAi : Player
    {
        public AiPlayerType type = AiPlayerType.RANDOM;
        public string name;
        public string getName()
        {
            return name;
        }

        public BoardGame makeMove(BoardGame game)
        {
            throw new NotImplementedException();
        }
    }
}
