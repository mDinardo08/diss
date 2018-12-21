using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game
{
    public class Move
    {
        public Move next;
        public Point2D possition;
        public Player owner;
    }
}
