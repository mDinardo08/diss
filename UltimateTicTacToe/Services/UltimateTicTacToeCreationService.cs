using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToe.Services
{
    public class UltimateTicTacToeCreationService : BoardCreationService
    {
        public IWinChecker winChecker;
        public IPlayerCreationService playerCreationService;
        public UltimateTicTacToeCreationService(IWinChecker winChecker, IPlayerCreationService playerCreationService)
        {
            this.winChecker = winChecker;
            this.playerCreationService = playerCreationService;
        }

        public BoardGame createBoardGame(BoardGameDTO gameDto)
        {
            return createTicTacToe(gameDto.game, winChecker);
        }

        private TicTacToe createTicTacToe(List<List<JObject>> JObjectBoard, IWinChecker winCheck)
        {
            TicTacToe result = new TicTacToe(winCheck);
            List<List<BoardGame>> board = new List<List<BoardGame>>();
            for (int row = 0; row < JObjectBoard.Count; row++)
            {
                board.Add(new List<BoardGame>());
                for (int col = 0; col < JObjectBoard[row].Count; col++)
                {
                    JObject space = JObjectBoard[row][col];
                    if (space["board"] == null)
                    {
                        board[row].Add(createTile(space));   
                    }
                    else
                    {
                        board[row].Add(createTicTacToe(space["board"].ToObject<List<List<JObject>>>(), winCheck));
                    }
                }
            }
            result.board = board;
            return result;
        }

        private Tile createTile(JObject jObject)
        {
            Tile t = new Tile();
            t.owner = playerCreationService.createPlayer(jObject["owner"] as JObject);
            return t;
        }
    }
}
