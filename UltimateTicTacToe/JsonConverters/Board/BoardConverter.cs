using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToe.JsonConverters.Board
{
    public class BoardConverter : AbstractJsonConverter<BoardGame> {

        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialisation");
        }

        protected override BoardGame Create(Type objectType, JObject jObject)
        {
            BoardGame type;
            if (jObject["board"] == null)
            {
                type = new Tile();
            }
            else
            {
                type = new TicTacToe(null);
            }
            return type;
        }
    }
}
