using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.GameClasses
{
    public class Tile : Game
    {
        private Player owner;

        public List<List<Game>> getGame()
        {
            throw new NotImplementedException();
        }

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
