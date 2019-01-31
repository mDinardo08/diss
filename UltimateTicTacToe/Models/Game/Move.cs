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
    }
}
