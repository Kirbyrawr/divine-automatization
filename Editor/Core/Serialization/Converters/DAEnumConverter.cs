using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class DAEnumConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        DAEnum enumVariable = (DAEnum)value;

        writer.WriteStartObject();
        writer.WritePropertyName("name");
        writer.WriteValue(enumVariable.name);

        writer.WritePropertyName("reference");
        writer.WriteValue(enumVariable.reference);

        writer.WritePropertyName("mutable");
        writer.WriteValue(enumVariable.mutable);

        writer.WritePropertyName("type");
        serializer.Serialize(writer, enumVariable.type);

        writer.WritePropertyName("value");
        writer.WriteValue(enumVariable.value.ToString());
        writer.WriteEndObject();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var json = JObject.ReadFrom(reader) as JObject;
        DAEnum enumVariable = new DAEnum();
        enumVariable.name = json["name"].ToString();
        enumVariable.reference = json["reference"].ToString();
        enumVariable.mutable = json["mutable"].ToObject<bool>();
        enumVariable.type = json["type"].ToObject<Type>();
        enumVariable.value = (Enum)Enum.Parse(enumVariable.type, json["value"].ToString());
        return enumVariable;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DAEnum);
    }
}
