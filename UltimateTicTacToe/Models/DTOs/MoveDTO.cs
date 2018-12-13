using Newtonsoft.Json;
using UltimateTicTacToe.JsonConverters.Board;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Models.DTOs
{
    public class MoveDTO
    {
        [JsonConverter(typeof(BoardConverter))]
        public BoardGame game;
        public Move move;
    }
}
