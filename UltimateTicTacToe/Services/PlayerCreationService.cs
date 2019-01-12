using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class PlayerCreationService : IPlayerCreationService
    {
        public IRandomService randomService;

        public PlayerCreationService(IRandomService random)
        {
            this.randomService = random;
        }

        public Player createPlayer(JObject jObject)
        {
            Player result = null;
            if(jObject?["type"].Equals(JToken.FromObject(PlayerType.RANDOM))??false)
            {
                result = new RandomAi(randomService);
            }
            return result;
        }

        public Player createPlayer(PlayerType rANDOM)
        {
            return new RandomAi(randomService);
        }
    }
}
