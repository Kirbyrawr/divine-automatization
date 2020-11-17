using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public class DAGameObjectConverter : JsonConverter<DAGameObject>
{
    public override void WriteJson(JsonWriter writer, DAGameObject value, JsonSerializer serializer)
    {
        DAGameObject variable = value;

        writer.WriteStartObject();
        writer.WritePropertyName("name");
        writer.WriteValue(variable.name);

        writer.WritePropertyName("reference");
        writer.WriteValue(variable.reference);

        writer.WritePropertyName("mutable");
        writer.WriteValue(variable.mutable);

        writer.WritePropertyName("value");
        writer.WriteValue(variable.value?.GetInstanceID());
        writer.WriteEndObject();
    }

    public override DAGameObject ReadJson(JsonReader reader, Type objectType, DAGameObject existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var json = JObject.ReadFrom(reader) as JObject;
        var data = new DAGameObject
        {
            name = json["name"].ToString(),
            reference = json["reference"].ToString(),
            mutable = json["mutable"].ToObject<bool>()
        };

        if (!string.IsNullOrEmpty(json["value"].ToString()))
        {
            data.value = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GetAssetPath(json["value"].ToObject<int>()));
        }

        return data;
    }
}
