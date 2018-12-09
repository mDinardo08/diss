using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.GameClasses
{
    public class WinChecker
    {
        private Func<Game, Point> check;
        private WinChecker successor;
        public Player checkForWin(Game game)
        {
            Point p = check(game);
            Player result = null;
            if (check(game) != null)
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
