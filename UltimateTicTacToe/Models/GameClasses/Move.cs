using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.GameClasses
{
    public class Move
    {
        public Move next;
        public System.Drawing.Point move;
        public Player owner;
    }
}
