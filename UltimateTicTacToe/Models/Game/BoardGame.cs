using Newtonsoft.Json;
using UltimateTicTacToe.JsonConverters.Board;

namespace UltimateTicTacToe.Models.Game
{
    [JsonConverter(typeof(BoardConverter))]
    public interface BoardGame
    { 
        Player getWinner();
        void makeMove(Move move);
    }
}
