﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game
{
    public class Tile : BoardGame
    {
        private Player owner;
       
        public Player getWinner()
        {
            return owner;
        }

        public bool isLeaf()
        {
            throw new NotImplementedException();
        }

        public void makeMove(Move move)
        {
            owner = move.owner;
        }
    }
}
