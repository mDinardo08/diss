using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.JsonConverters
{
public abstract class AbstractJsonConverter<T>: JsonConverter
{

        public override bool CanWrite => false;
        public override bool CanRead => true;

        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }
        
        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                            object existingValue,
                                            JsonSerializer serializer)
        {

            if (reader.TokenType == JsonToken.Null) return null;
            JObject jObject = JObject.Load(reader);
            T target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }
    }
    
}
