using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class GoodDadPlayerClassHandler : AbstractPlayerClassHandler
    {
        public GoodDadPlayerClassHandler(IRandomService randomService, IDatabaseProvider provider) : base(randomService, provider)
        {
            type = PlayerType.GOODDAD;
            successor = new MineFieldPlayerClassHandler(randomService, provider);
        }

        protected override Player buildPlayer()
        {
            return new GoodDad(randomService, provider);
        }
    }
}
