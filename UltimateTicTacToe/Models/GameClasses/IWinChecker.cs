using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.GameClasses
{
    public interface IWinChecker
    {
        Player checkForWin(Game game);
    }
}
