using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.JsonConverters.AiPlayer
{
    public class PlayerConverter : AbstractJsonConverter<Player>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialisation");
        }

        protected override Player Create(Type objectType, JObject jObject)
        {
            return new RandomAi(new RandomService());
        }
    }
}
