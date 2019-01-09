using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class GameService : IGameService
    {
        public BoardGameDTO processMove(BoardGame game, Player Ai)
        {
            BoardGameDTO result = new BoardGameDTO();
            result.next = Ai;
            try
            {
                result.Winner = game.getWinner();
            }
            catch (NoWinnerException)
            {
                result.game = Ai.makeMove(game);
                try
                {
                    result.Winner = game.getWinner();
                }
                catch (NoWinnerException) { }
            }
            return result;
        }
    }
}
