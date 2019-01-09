using Newtonsoft.Json;
using UltimateTicTacToe.JsonConverters.Board;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.DTOs
{
    public class BoardGameDTO
    {
        public BoardGame game;
        public Player Winner;
        public Player next;
    }
}
