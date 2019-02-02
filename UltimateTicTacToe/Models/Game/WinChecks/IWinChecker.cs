using Newtonsoft.Json;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game.WinCheck
{
    public interface IWinChecker
    {
        PlayerColour? checkForWin(BoardGame game);
    }
}
