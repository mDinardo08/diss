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
        public PlayerClassHandler classHandler;
        public PlayerCreationService(PlayerClassHandler classHandler)
        {
            this.classHandler = classHandler;
        }

        public Player createPlayer(JObject jObject)
        {
            return createPlayer(jObject.GetValue("type").ToObject<PlayerType>());
        }

        public Player createPlayer(PlayerType type)
        {
            return classHandler.createPlayer(type);
        }
    }
}
