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
        List<List<Game>> getGame();
        Game getSector(Point point);
        void setBoard(List<List<Game>> board);
    }
}
