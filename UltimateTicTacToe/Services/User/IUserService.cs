using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.DTOs;

namespace UltimateTicTacToe.Services.User
{
    public interface IUserService
    {
        RatingDTO createUser();
        RatingDTO getUser(int UserId);
        RatingDTO updateUser(int UserId, double score);
    }
}
