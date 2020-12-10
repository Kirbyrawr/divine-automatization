using System;
using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DAInspectorGameObjectField : DAInspectorField
{
    private DAGameObject _variable;

    public DAInspectorGameObjectField(string title, DAGameObject variable)
    {
        AddToClassList("inspector-field");

        _variable = variable;

        Add(PropertiesButton());

        ObjectField input = new ObjectField() { label = title, name = "input", objectType = typeof(GameObject), allowSceneObjects = false };
        input.style.flexShrink = 1;
        Add(input);

        RefreshField();

        input.RegisterValueChangedCallback((evt) =>
        {
            DAEditor.Instance.graphView.GraphObject.RegisterCompleteObjectUndo(title);
            _variable.value = (GameObject)evt.newValue;
        });
    }

    public override void SetReference(string reference)
    {
        DAEditor.Instance.graphView.GraphObject.RegisterCompleteObjectUndo("Set Property");
        _variable.value = null;
        _variable.reference = reference;
        RefreshField();
        DAEditor.Instance.graphView.Serialize();
    }

    private void RefreshField()
    {
        var input = this.Q<ObjectField>("input");

        if (string.IsNullOrEmpty(_variable.reference))
        {
            input.SetValueWithoutNotify(_variable.value);
            input.SetEnabled(true);
        }
        else
        {
            //input.SetValueWithoutNotify(_variable.reference);
            input.SetEnabled(false);
        }
    }

    protected override DAPropertyData GetVariable()
    {
        return _variable;
    }
}
