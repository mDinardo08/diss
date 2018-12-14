using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToe.JsonConverters.Board
{
    public class BoardConverter : JsonConverter
    {

        public override bool CanWrite => false;
        public override bool CanRead => true;
    
        public BoardConverter()
        {

        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(BoardGame).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = Newtonsoft.Json.Linq.JObject.Load(reader);
            BoardGame type;
            if(jObject["board"] == null)
            {
                type = new Tile();
            } else
            {
                type = new TicTacToe(null);
            }
            serializer.Populate(jObject.CreateReader(), type);
            return type;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialisation");
        }
    }
}
