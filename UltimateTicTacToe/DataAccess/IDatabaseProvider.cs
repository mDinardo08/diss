using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.DTOs;

namespace UltimateTicTacToe.DataAccess
{
    public interface IDatabaseProvider
    {

        RatingDTO getUser(int UserId);
        int createUser();
        RatingDTO updateUser(int UserId, double LatestScore);
    }
}
