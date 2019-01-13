using Newtonsoft.Json;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game.WinCheck
{
    public interface IWinChecker
    {
        Player checkForWin(BoardGame game);
    }
}
