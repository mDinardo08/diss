using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.JsonConverters.WinCheck;

namespace UltimateTicTacToe.Models.Game.WinCheck
{

    [JsonConverter(typeof(WinCheckConverter))]
    public interface IWinChecker
    {
        Player checkForWin(CompositeGame game);
    }
}
