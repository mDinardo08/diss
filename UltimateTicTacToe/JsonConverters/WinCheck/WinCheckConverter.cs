using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToe.JsonConverters.WinCheck
{
    public class WinCheckConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IWinChecker).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
