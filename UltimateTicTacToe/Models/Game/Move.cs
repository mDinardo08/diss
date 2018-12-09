using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game
{
    public class Move
    {
        public Move next;
        public System.Drawing.Point move;
        public Player owner;
    }
}
