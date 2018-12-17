using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game
{
    public class Tile : BoardGame
    {
        public Player owner;
       
        public Player getWinner()
        {
            return owner;
        }

        public void makeMove(Move move)
        {
            owner = move.owner;
        }
    }
}
