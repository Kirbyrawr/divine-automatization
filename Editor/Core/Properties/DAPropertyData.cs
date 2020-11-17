using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

public abstract class DAPropertyData
{
    public string name;
    public string reference = null;
    public bool mutable = true;
    public abstract object GetValue();
    public abstract System.Type GetValueType();
}

public abstract class DAPropertyData<T> : DAPropertyData
{
    public T value;
    public virtual T GetValue(Dictionary<string, object> properties)
    {
        if (string.IsNullOrEmpty(reference))
        {
            return value;
        }
        else if (mutable)
        {
            return (T)properties[reference];
        }

        return default(T);
    }

    public override object GetValue()
    {
        return value;
    }

    public override System.Type GetValueType()
    {
        return typeof(T);
    }
}
