using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.GameClasses
{
    public interface Game
    {
        Player getWinner();
        void makeMove(Move move);
        List<List<Game>> getBoard();
        Game getSector(Point point);
        void setBoard(List<List<Game>> board);
    }
}
