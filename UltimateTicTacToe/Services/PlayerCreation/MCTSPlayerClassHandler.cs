using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class MCTSPlayerClassHandler : AbstractPlayerClassHandler
    {

        public MCTSPlayerClassHandler(IRandomService randomService, IDatabaseProvider provider) : base(randomService, provider)
        {
            type = PlayerType.MCTS;
            successor = new GoodDadPlayerClassHandler(randomService, provider);
        }

        protected override Player buildPlayer()
        {
            return new MCTSPlayer(randomService, provider);
        }
    }
}
