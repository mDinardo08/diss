using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class MineFieldPlayerClassHandler : AbstractPlayerClassHandler
    {
        public MineFieldPlayerClassHandler(IRandomService randomService, IDatabaseProvider provider) : base(randomService, provider)
        {
            type = PlayerType.MINEFIELD;
        }

        protected override Player buildPlayer()
        {
            return new MineFieldPlayer(randomService, provider);
        }
    }
}
