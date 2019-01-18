using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Models.DTOs
{
    public class BoardCreationDto
    {
        public int size;
        public List<JObject> players;
    }
}
