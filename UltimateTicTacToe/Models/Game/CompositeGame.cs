using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game
{
    interface CompositeGame: BoardGame
    {

        List<List<BoardGame>> getBoard();
        BoardGame getSector(Point point);
        void setBoard(List<List<BoardGame>> board);
    }
}
