using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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
            result.next = 0;
            try
            {
                result.Winner = game.getWinner();
            }
            catch (NoWinnerException)
            {

                //result.game = Ai.makeMove(game).getBoard();
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
