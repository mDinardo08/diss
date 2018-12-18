using Newtonsoft.Json;
using UltimateTicTacToe.JsonConverters.Board;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Models.DTOs
{
    public class MoveDTO
    {
        public BoardGame game;
        public Move move;
        public bool gameOver;
    }
}
