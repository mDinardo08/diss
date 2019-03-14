using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess.DBOs;

namespace UltimateTicTacToe.Models.DTOs
{
    public class RatingDTO
    {
        public int userId;
        public double latest;
        public double highOption;
        public double lowOption;
        public double average;
        public List<MoveDBO> moves;
    }
}
