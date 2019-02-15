using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public interface IPlayerCreationService
    {
        Player createPlayer(JObject jObject);
        Player createPlayer(PlayerType rANDOM);
        List<Player> createPlayers(List<JObject> jObjects);
    }
}
