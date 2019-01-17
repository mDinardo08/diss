using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToe.Services
{
    public class GameService : IGameService
    {
        private IWinChecker winChecker;

        public GameService(IWinChecker winChecker)
        {
            this.winChecker = winChecker;
        }

        public BoardGameDTO processMove(BoardGame game, Player Ai)
        {
            BoardGameDTO result = new BoardGameDTO();
            result.cur = new JObject();
            result.cur.Add("type", JToken.FromObject(Ai.getPlayerType()));
            try
            {
                result.Winner = game.getWinner();
            }
            catch (NoWinnerException)
            {
                List<List<BoardGame>> board = Ai.makeMove(game).getBoard();
                result.game = convertToJObject(board);
                try
                {
                    result.Winner = game.getWinner();
                }
                catch (NoWinnerException) { }
            }
            return result;
        }

        private List<List<JObject>> convertToJObject(List<List<BoardGame>> board)
        {
            List<List<JObject>> result = new List<List<JObject>>();
            for(int x = 0; x< board.Count; x++)
            {
                result.Add(new List<JObject>());
                for(int y = 0; y < board[x].Count; y++)
                {
                    result[x].Add(JObject.FromObject(board[x][y]));
                }
            }
            return result;

        }
    }
}
