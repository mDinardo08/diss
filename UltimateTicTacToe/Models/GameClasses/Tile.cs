using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.GameClasses
{
    public class Tile : BoardGame
    {
        private Player owner;

        public List<List<BoardGame>> getBoard()
        {
            throw new NotImplementedException();
        }

        public BoardGame getSector(Point point)
        {
            throw new NotImplementedException();
        }

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

        public void setBoard(List<List<BoardGame>> board)
        {
            throw new NotImplementedException();
        }
    }
}
