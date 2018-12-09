using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game
{
    public interface BoardGame
    {
        Player getWinner();
        void makeMove(Move move);
        bool isLeaf();
        List<List<BoardGame>> getBoard();
        BoardGame getSector(Point point);
        void setBoard(List<List<BoardGame>> board);
    }
}
