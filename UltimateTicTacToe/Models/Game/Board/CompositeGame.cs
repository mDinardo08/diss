using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game
{
    public abstract class CompositeGame: AbstractBoard
    {
        public BoardGame getSector(Point2D point)
        {
            return board[point.X][point.Y];
        }
        public void setBoard(List<List<BoardGame>> board)
        {
            this.board = board;
        }
    }
}
