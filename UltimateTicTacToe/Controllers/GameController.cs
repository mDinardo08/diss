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
        public IActionResult makeMove([FromBody]MoveDTO moveDto)
        {
            return ExecuteApiAction(() => new ApiResult<BoardGame> { Model = service.makeMove(moveDto.game, moveDto.move) });
        }
    }
}