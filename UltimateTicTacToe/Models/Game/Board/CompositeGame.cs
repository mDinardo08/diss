using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game
{
    public abstract class CompositeGame: AbstractBoard
    {

        public Point2D boardFilter;
        public List<List<BoardGame>> board;

        public override List<List<BoardGame>> getBoard()
        {
            return board;
        }

        public BoardGame getSector(Point2D point)
        {
            return board[point.X][point.Y];
        }
        public void setBoard(List<List<BoardGame>> board)
        {
            this.board = board;
        }

        public override void registerMove(Move move)
        {
            if (move?.next.possition != null)
            {
                boardFilter = move.next.possition;
                getSector(boardFilter).registerMove(move.next);
            }
        }
    }
}
