using Newtonsoft.Json;
using System.Collections.Generic;
using UltimateTicTacToe.JsonConverters.Board;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game
{
    [JsonConverter(typeof(BoardConverter))]
    public interface BoardGame
    { 
        Player getWinner();
        void makeMove(Move move);
        List<Move> getAvailableMoves();
        List<List<BoardGame>> getBoard();
    }
}
