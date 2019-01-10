using Newtonsoft.Json;
using System.Collections.Generic;
using UltimateTicTacToe.JsonConverters.Board;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.DTOs
{
    public class BoardGameDTO
    {
        public List<List<BoardGame>> game;
        public Player Winner;
        public PlayerType next;
        public Move lastMove;
    }
}
