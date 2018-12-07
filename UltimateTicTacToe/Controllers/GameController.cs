using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UltimateTicTacToe.Models;

namespace UltimateTicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : BaseController
    {

        [HttpPost("makeMove")]
        public IActionResult makeMove([FromBody]Game game)
        {
            return null;
        }
    }
}