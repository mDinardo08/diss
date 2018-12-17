using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToe.JsonConverters.WinCheck
{
    public class WinCheckConverter : AbstractJsonConverter<IWinChecker>
    {
        public override bool CanWrite => true;
        public override bool CanRead => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var winCheck = value as WinCheckHandler;
            writer.WriteStartObject();
            writer.WriteEndObject();
        }

        protected override IWinChecker Create(Type objectType, JObject jObject)
        {
            return new HorizontalWinChecker();
        }
    }
}
