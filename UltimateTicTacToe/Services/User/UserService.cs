using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.DTOs;

namespace UltimateTicTacToe.Services.User
{
    public class UserService: IUserService
    {
        private IDatabaseProvider provider;

        public UserService(IDatabaseProvider provider)
        {
            this.provider = provider;
        }

        public RatingDTO createUser()
        {
            return provider.getUser(provider.createUser());
        }

        public RatingDTO getUser(int UserId)
        {
            return provider.getUser(UserId);
        }

        public RatingDTO updateUser(int UserId, double score)
        {
            return provider.updateUser(UserId, score);
        }
    }
}
