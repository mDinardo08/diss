using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game
{
    public abstract class AbstractBoard : BoardGame
    {
        public PlayerColour? owner;

        public List<List<BoardGame>> board;

        public List<List<BoardGame>> getBoard()
        {
            return board;
        }

        public abstract List<Move> getAvailableMoves();

        public abstract PlayerColour? getWinner();

        public abstract void makeMove(Move move);
        public abstract void validateBoard();

        public bool isWon()
        {
            return owner != null; 
        }

        public bool isDraw()
        {
            return owner == null && getAvailableMoves().Count == 0;
        }

        public abstract void registerMove(Move move);
    }
}
