using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    abstract public class AbstractPlayerClassHandler : PlayerClassHandler
    {
        public PlayerType type;
        public PlayerClassHandler successor;
        public IRandomService randomService;
        public AbstractPlayerClassHandler(IRandomService randomService)
        {
            this.randomService = randomService;
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
