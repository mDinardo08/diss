using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public class PlayerCreationService :  IPlayerCreationService
    {
        public PlayerClassHandler classHandler;
        public PlayerCreationService(PlayerClassHandler classHandler)
        {
            this.classHandler = classHandler;
        }

        public Player createPlayer(JObject jObject)
        {
            return jObject == null ? null : createPlayerFromJObject(jObject);
        }

        public Player createPlayer(PlayerType type)
        {
            return classHandler.createPlayer(type);
        }

        public List<Player> createPlayers(List<JObject> jObjects)
        {
            List<Player> result = new List<Player>();
            foreach(JObject player in jObjects)
            {
                result.Add(createPlayer(player));
            }
            return result;
        }

        private Player createPlayerFromJObject(JObject player)
        {
            Player result = createPlayer(player["type"].ToObject<PlayerType>());
            result.setName(player["name"].ToObject<string>());
            return result;
        }
    }
}
