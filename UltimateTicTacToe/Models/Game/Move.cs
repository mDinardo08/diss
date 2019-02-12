using System;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game
{
    public class Move
    {
        public Move next;
        public Point2D possition;
        public PlayerColour? owner;

        public void setOwner(PlayerColour player)
        {
            owner = player;
            next?.setOwner(player);
        }

        public bool Equals(Move m)
        {
            bool result = false;
            if (next == null & m.next == null)
            {
                result = EqualPossition(m.possition)
                     && owner == m.owner;
            } else if (m.next != null && next != null)
            {
                result = next.Equals(m.next) && EqualPossition(m.possition);
            } else
            {
                result = false;
            }
            return result;
        }

        private bool EqualPossition(Point2D pos)
        {
            bool result = false;
            if (pos != null)
            {
                result = possition.X == pos.X && possition.Y == pos.Y;
            } else
            {
                result = pos == possition;
            }
            return result ;
        }
    }
}
