using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game.WinCheck
{
    public abstract class WinCheckHandler: IWinChecker
    {
        public Func<BoardGame, Point> check;
        public IWinChecker successor;

        public WinCheckHandler()
        {
            init();
        }

        public PlayerColour? checkForWin(BoardGame game)
        {
            Point p = check(game);
            PlayerColour? result = null;
            if (p.X != -1)
            {
                result = game.getBoard()[p.X][p.Y].getWinner();
            } else if (successor != null)
            {
                result = successor.checkForWin(game);
            }
            return result;
        }

        private void init()
        {
            setSuccessor();
            setCheckFunction();
        }

        public abstract void setSuccessor();

        public abstract void setCheckFunction();

        public IWinChecker getSuccessor()
        {
            return successor;
        }
    }
}
