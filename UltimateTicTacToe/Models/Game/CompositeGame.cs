using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game
{
    public interface CompositeGame: BoardGame
    {

        List<List<BoardGame>> getBoard();
        BoardGame getSector(Point2D point);
        void setBoard(List<List<BoardGame>> board);
    }
}
