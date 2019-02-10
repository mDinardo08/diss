using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Services.User;

namespace UltimateTicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] int UserId)
        {
            return ExecuteApiAction(() => new ApiResult<RatingDTO> { Model = userService.getUser(UserId) });
        }

        [HttpPost("createUser")]
        public IActionResult createUser()
        {
            return ExecuteApiAction(() => new ApiResult<RatingDTO> { Model = userService.createUser() });
        }
    }
}
