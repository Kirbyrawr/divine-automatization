using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using System;
using Newtonsoft.Json;

public abstract class DABlackboardProperty : VisualElement
{
    public abstract string TypeString { get; }
    public abstract DAPropertyData GetData();
    public abstract string Serialize();
    public abstract void Deserialize(string json);
}

public abstract class DABlackboardProperty<T> : DABlackboardProperty where T : DAPropertyData
{
    protected DABlackboard _blackboard;
    protected T _data;

    public DABlackboardProperty(DABlackboard blackboard)
    {
        _data = Activator.CreateInstance(typeof(T)) as T;
        _data.name = TypeString;
        _data.reference = $"{TypeString}_{DAUtils.GenerateShortID()}";
        _blackboard = blackboard;

        //Reference
        var referenceField = new TextField() { name = "reference", label = "Reference" };
        Add(referenceField);

        //Exposed
        var exposedField = new Toggle("Mutable") { name = "mutable" };
        Add(exposedField);
    }

    protected virtual void RefreshValues()
    {
        this.Q<TextField>("reference").SetValueWithoutNotify(_data.reference);
        this.Q<Toggle>("mutable").SetValueWithoutNotify(_data.mutable);
    }

    public override string Serialize()
    {
        if (_data == null)
        {
            return "";
        }
        else
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.None;
            return JsonConvert.SerializeObject(_data, settings);
        }
    }

    public override void Deserialize(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            _data = null;
        }
        else
        {
            _data = JsonConvert.DeserializeObject<T>(json);
            RefreshValues();
        }
    }

    public override DAPropertyData GetData()
    {
        return _data;
    }
}
