using Newtonsoft.Json;
using System.Collections.Generic;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game
{
    public class Tile : AbstractBoard
    {
        public Player owner;
        public new List<List<BoardGame>> board = null;
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
