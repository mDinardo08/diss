using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    abstract public class AbstractPlayerClassHandler : PlayerClassHandler
    {
        public PlayerType type;
        public PlayerClassHandler successor;
        public IRandomService randomService;
        public IDatabaseProvider provider;
        public AbstractPlayerClassHandler(IRandomService randomService, IDatabaseProvider provider)
        {
            this.randomService = randomService;
            this.provider = provider;
        }

        public Player createPlayer(PlayerType type){
            Player result = null;
            if(this.type == type)
            {
                result = buildPlayer();
            }else
            {
                result = successor?.createPlayer(type);
            }
            return result;
        }

        protected abstract Player buildPlayer();
    }
}
