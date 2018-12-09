using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.GameClasses
{
    public class Tile : Game
    {
        private Player owner;

        public List<List<Game>> getBoard()
        {
            throw new NotImplementedException();
        }

        public Game getSector(Point point)
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

        public void setBoard(List<List<Game>> board)
        {
            throw new NotImplementedException();
        }
    }
}
