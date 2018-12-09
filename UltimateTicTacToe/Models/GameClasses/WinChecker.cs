using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.GameClasses
{
    public abstract class WinCheckHandler: IWinChecker
    {
        public Func<Game, Point> check;
        public WinCheckHandler successor;
        public Player checkForWin(Game game)
        {
            Point p = check(game);
            Player result = null;
            if (p.X != -1)
            {
                result = game.getGame()[p.X][p.Y].getWinner();
            } else if (successor != null)
            {
                result = successor.checkForWin(game);
            }
            return result;
        }

    }
}
