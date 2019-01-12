using Newtonsoft.Json;
using UltimateTicTacToe.JsonConverters.WinCheck;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game.WinCheck
{

    [JsonConverter(typeof(WinCheckConverter))]
    public interface IWinChecker
    {
        Player checkForWin(BoardGame game);
    }
}
