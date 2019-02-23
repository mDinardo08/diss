using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.Game.WinCheck;
using UltimateTicTacToe.Models.MCTS;

namespace UltimateTicTacToe.Services
{
    public class GameService : IGameService
    {
        private NodeService nodeService;
        private IDatabaseProvider provider;
        public GameService(NodeService nodeService, IDatabaseProvider provider)
        {
            this.nodeService = nodeService;
            this.provider = provider;
        }

        public BoardGameDTO processMove(BoardGame game, Player cur, List<Player> players)
        {
            BoardGameDTO result = new BoardGameDTO();
            result.players = buildPlayersArray(players);
            if (game.isWon())
            {
                result.Winner = game.getWinner();
                SaveGame(players, game.getWinner());
            }else
            {
                if (cur.getPlayerType() != PlayerType.HUMAN)
                {
                    HandleAiMove(cur, game, result, players);
                    if (game.isWon())
                    {
                        result.Winner = game.getWinner();
                        SaveGame(players, result.Winner);
                    }
                }
                else
                {
                    result.cur = convertToJObject(cur);
                    result.game = convertToJObject(game.getBoard());
                }
            }
            result.game = convertToJObject(game.getBoard());
            result.availableMoves = game.getAvailableMoves();
            return result;
        }

        private void HandleAiMove(Player Ai, BoardGame game, BoardGameDTO result, List<Player> players)
        {
            List<INode> nodes = nodeService.process(game, Ai.getColour());
            Player next = players.Find(x => !x.getColour().Equals(Ai.getColour()));
            INode move = Ai.makeMove(game.Clone() as BoardGame, nodes, next.getUserId());
            provider.updateUser(Ai.getUserId(), move.getReward());
            game.makeMove(move.getMove());
            List<List<BoardGame>> board = game.getBoard();
            result.lastMove = move.getMove();
            result.cur = convertToJObject(next);
            result.lastMoveRating = move.getReward();
            int place = 0;
            double high = -1;
            double low = 1;
            foreach(INode node in nodes)
            {
                double reward = node.getReward();
                if (reward > high)
                {
                    high = reward;
                }
                if (reward < low)
                {
                    low = reward;
                }
                if (move.getReward() < reward)
                {
                    place++;
                }
            }
            provider.saveMove(Ai.getUserId(), move.getReward(), place);
            result.highOption = high;
            result.lowOption = low;
        }
    
        private List<JObject> buildPlayersArray(List<Player> players)
        {
            List<JObject> result = new List<JObject>();
            foreach(Player player in players)
            {
                result.Add(convertToJObject(player));
            }
            return result;
        }

        private JObject convertToJObject(Player player)
        {
            JObject jPlayer = new JObject();
            if (player != null)
            {
                jPlayer.Add("type", JToken.FromObject(player.getPlayerType()));
                jPlayer.Add("name", JToken.FromObject(player.getName()));
                jPlayer.Add("colour", JToken.FromObject(player.getColour()));
                jPlayer.Add("userId", JToken.FromObject(player.getUserId()));
            }
            return jPlayer;
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

        public RatingDTO rateMove(BoardGame boardGame, Move move, int UserId, Move lastMove)
        {
            RatingDTO result = null;
            boardGame.registerMove(lastMove);
            boardGame.validateBoard();
            double high = -1;
            double low = 1;
            List<INode> nodes = nodeService.process(boardGame, (PlayerColour)move.owner);
            foreach (INode node in nodes)
            {
                double reward = node.getReward();
                if (reward > high)
                {
                    high = reward;
                }
                if (reward < low)
                {
                    low = reward;
                }
                if (node.getMove().Equals(move))
                {
                    result = provider.updateUser(UserId, node.getReward());

                    if (result != null)
                    {
                        result.highOption = high;
                        result.lowOption = low;
                    }
                }
            }
            return result;
        }
        

        private void SaveGame(List<Player> players, PlayerColour? Winner)
        {
            if (players.TrueForAll(x => x.getUserId() >= 0))
            {
                Player winner = players.Find(x => x.getColour() == Winner);
                if ( winner == null)
                {
                    provider.saveGameResult(players[0].getUserId(), players[1].getUserId(), null);
                } else
                {
                    provider.saveGameResult(players[0].getUserId(), players[1].getUserId(), winner.getUserId());
                }
            }
        }
    }
}
