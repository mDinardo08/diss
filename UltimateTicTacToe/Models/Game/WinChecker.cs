using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game
{
    public abstract class WinCheckHandler: IWinChecker
    {
        public Func<BoardGame, Point> check;
        public WinCheckHandler successor;
        public Player checkForWin(BoardGame game)
        {
            Point p = check(game);
            Player result = null;
            if (p.X != -1)
            {
                result = game.getBoard()[p.X][p.Y].getWinner();
            } else if (successor != null)
            {
                result = successor.checkForWin(game);
            }
            return result;
        }

    }
}
