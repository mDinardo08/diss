using Newtonsoft.Json;
using System.Collections.Generic;
using UltimateTicTacToe.JsonConverters.AiPlayer;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game
{
    public class Tile : AbstractBoard
    {
        [JsonConverter(typeof(PlayerConverter))]
        public Player owner;

        public override List<Move> getAvailableMoves()
        {
            return owner == null ? new List<Move> { new Move() } : new List<Move>();
        }

        public override Player getWinner()
        {
            return owner;
        }

        public override void makeMove(Move move)
        {
            owner = move.owner;
        }
    }
}
