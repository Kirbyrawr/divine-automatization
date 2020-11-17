using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(DAEnumConverter))]
[System.Serializable]
public class DAEnum : DAPropertyData<Enum>
{
    public enum Empty { Empty };

    public Type type = typeof(Empty);

    public DAEnum()
    {
        value = Empty.Empty;
    }

    public DAEnum(Enum enumValue)
    {
        type = enumValue.GetType();
        value = enumValue;
    }

    public override System.Type GetValueType()
    {
        return type;
    }
}
