using Newtonsoft.Json;
using System.Collections.Generic;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.Game
{
    public interface BoardGame
    { 
        PlayerColour getWinner();
        void makeMove(Move move);
        List<Move> getAvailableMoves();
        List<List<BoardGame>> getBoard();
        void validateBoard();
        bool isWon();
        bool isDraw();
        void registerMove(Move move);
    }
}
