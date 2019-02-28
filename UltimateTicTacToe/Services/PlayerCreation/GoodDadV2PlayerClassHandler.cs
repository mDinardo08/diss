using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services.PlayerCreation
{
    public class GoodDadV2PlayerClassHandler : AbstractPlayerClassHandler
    {
        public GoodDadV2PlayerClassHandler(IRandomService randomService, IDatabaseProvider provider) : base(randomService, provider)
        {
            type = PlayerType.GOODDADV2;
        }

        protected override Player buildPlayer()
        {
            return new GoodDadV2(randomService, provider);
        }
    }
}
