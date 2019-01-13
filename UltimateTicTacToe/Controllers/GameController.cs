using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
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

        [HttpPost("makeMove")]
        public IActionResult makeMove([FromBody]BoardGameDTO gameDto)
        {
            BoardGame game = boardCreationService.createBoardGame(gameDto);
            Player player = playerCreationService.createPlayer(gameDto.next);
            return ExecuteApiAction(() => new ApiResult<BoardGameDTO> { Model = gameService.processMove(game, player) });
        }
    }
}