using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game.WinCheck
{
    public abstract class WinCheckHandler: IWinChecker
    {
        public Func<CompositeGame, Point> check;
        [JsonIgnore]
        public IWinChecker successor;

        public WinCheckHandler()
        {
            init();
        }

        public Player checkForWin(CompositeGame game)
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
