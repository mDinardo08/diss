using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : BaseController
    {
        private IGameService gameService;
        private BoardCreationService boardCreationService;
        private IPlayerCreationService playerCreationService;
        public GameController(IGameService gameService, BoardCreationService boardCreationService, IPlayerCreationService playerCreationService)
        {
            this.gameService = gameService;
            this.boardCreationService = boardCreationService;
            this.playerCreationService = playerCreationService;
        }

        [HttpPost("rateMove")]
        public IActionResult rateMove([FromBody] MoveDto moveDto)
        {
            BoardGame game = boardCreationService.createBoardGame(moveDto.game);
            return ExecuteApiAction(() => new ApiResult<RatingDTO> { Model = gameService.rateMove(game, moveDto.move, moveDto.UserId, moveDto.lastMove) });
        }

        [HttpPost("makeMove")]
        public IActionResult makeMove([FromBody]BoardGameDTO gameDto)
        {
            BoardGame game = boardCreationService.createBoardGame(gameDto);
            Player player = playerCreationService.createPlayer(gameDto.cur);
            List<Player> players = playerCreationService.createPlayers(gameDto.players);
            return ExecuteApiAction(() => new ApiResult<BoardGameDTO> { Model = gameService.processMove(game, player, players) });
        }

        [HttpPost("createBoard")]
        public IActionResult createBoard([FromBody]BoardCreationDto creationDto)
        {
            BoardGame board = boardCreationService.createBoardGame(creationDto.size);
            List<Player> players = playerCreationService.createPlayers(creationDto.players);
            return ExecuteApiAction(() => new ApiResult<BoardGameDTO> { Model = gameService.processMove(board, players[0], players) });
        }
    }
}