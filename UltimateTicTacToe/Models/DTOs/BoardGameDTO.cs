using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.DTOs
{
    public class BoardGameDTO
    {
        public List<List<JObject>> game;
        public PlayerColour? Winner;
        public List<JObject> players;
        public JObject cur;
        public Move lastMove;
        public List<Move> availableMoves;
        public double lastMoveRating;
    }
}
