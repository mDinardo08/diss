using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.DataAccess.DBOs
{
    public class RatingDBO
    {
        public int UserId;
        public double LatestScore;
        public double TotalScore;
        public int TotalMoves;
    }
}
