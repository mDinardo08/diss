using Newtonsoft.Json;
using System.Collections.Generic;
using UltimateTicTacToe.JsonConverters.AiPlayer;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game
{
    public class Tile : BoardGame
    {
        [JsonConverter(typeof(PlayerConverter))]
        public Player owner;

        public List<Move> getAvailableMoves()
        {
            return owner == null ? new List<Move> { null } : new List<Move>();
        }

        public Player getWinner()
        {
            return owner;
        }

        public void makeMove(Move move)
        {
            owner = move.owner;
        }
    }
}
