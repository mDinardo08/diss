using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : BaseController
    {
        private IGameService service;

        public GameController(IGameService service)
        {
            this.service = service;
        }

        [HttpPost("makeMove")]
        public IActionResult makeMove([FromBody]BoardGameDTO game)
        {
            return ExecuteApiAction(() => new ApiResult<BoardGameDTO> { Model = service.processMove(game.game, game.Ai) });
        }
    }
}