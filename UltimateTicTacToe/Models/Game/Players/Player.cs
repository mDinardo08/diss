using Newtonsoft.Json;
using UltimateTicTacToe.JsonConverters.AiPlayer;

namespace UltimateTicTacToe.Models.Game.Players
{
    [JsonConverter(typeof(PlayerConverter))]
    public interface Player
    {
        string getName();
        BoardGame makeMove(BoardGame game);
    }
}
