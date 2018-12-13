using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.JsonConverters.Board
{
    public class BoardConverter : JsonConverter
    {

        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return typeof(BoardGame).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = Newtonsoft.Json.Linq.JObject.Load(reader);
            var type = default(BoardGame);
            type = new TicTacToe(null);
            serializer.Populate(jObject.CreateReader(), type);
            return new TicTacToe(null);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialisation");
        }
    }
}
