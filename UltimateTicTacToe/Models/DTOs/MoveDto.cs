using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Models.DTOs
{
    public class MoveDto
    {
        Move move;
        public List<List<JObject>> game;
    }
}
