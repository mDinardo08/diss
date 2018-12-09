using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game.WinCheck
{
    public interface IWinChecker
    {
        Player checkForWin(CompositeGame game);
    }
}
