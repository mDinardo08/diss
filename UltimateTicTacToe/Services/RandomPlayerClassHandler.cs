using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class RandomPlayerClassHandler : AbstractPlayerClassHandler
    {
        public RandomPlayerClassHandler(IRandomService randomService, NodeService nodeService) : base(randomService, nodeService)
        {
            type = PlayerType.RANDOM;
            successor = new HumanPlayerClassHandler(randomService, nodeService);
        }

        protected override Player buildPlayer()
        {
            return new RandomAi(randomService);
        }
    }
}
