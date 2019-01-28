﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public abstract class AbstractPlayer : Player
    {
        public string name;
        public IRandomService random;
        public PlayerType type;
        public PlayerColour colour;
        public AbstractPlayer(IRandomService random)
        {
            this.random = random;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public PlayerType getPlayerType()
        {
            return type;
        }

        public void setColour(PlayerColour colour)
        {
            this.colour = colour;
        }

        public PlayerColour getColour()
        {
            return this.colour;
        }

        public Move makeMove(BoardGame game)
        {
            List<Move> possibleMoves = game.getAvailableMoves();
            Move decided = decideMove(game, possibleMoves);
            decided.setOwner(this);
            game.makeMove(decided);
            return decided;
        }

        abstract protected Move decideMove(BoardGame game, List<Move> moves);
    }
}
