using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class HumanPlayerClassHandler : AbstractPlayerClassHandler
    {
        public HumanPlayerClassHandler(IRandomService randomService, IDatabaseProvider provider) : base(randomService, provider)
        {
            type = PlayerType.HUMAN;
            successor = new MCTSPlayerClassHandler(randomService, provider);
        }

        protected override Player buildPlayer()
        {
            return new HumanPlayer(randomService, provider);
        }
    }
}
