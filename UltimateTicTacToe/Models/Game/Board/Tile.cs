using Newtonsoft.Json;
using System.Collections.Generic;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game
{
    public class Tile : AbstractBoard
    {

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override List<Move> getAvailableMoves()
        {
            return owner == null ? new List<Move> { new Move() } : new List<Move>();
        }

        public override List<List<BoardGame>> getBoard()
        {
            return null;
        }

        public override PlayerColour? getWinner()
        {
            return owner;
        }

        public override void makeMove(Move move)
        {
            owner = move.owner;
        }

        public override void registerMove(Move move)
        {
        }

        public override void validateBoard()
        {
        }
    }
}
